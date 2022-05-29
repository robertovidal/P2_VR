using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdDoctorController : MonoBehaviour
{

    private Animator myAnimator;
    private bool talkedFirstInstructions;
    public AudioSource instructions;
    public AudioSource keyNotGrabbed;
    public AudioSource instructionsNextRoom;
    private IEnumerator coroutine;
    private bool keyGrabbed;
    private bool finishedTalking;
    public GameObject key;
    private bool checkPositionZ;
    private bool checkPositionX;
    private float positionToCheck;
    public GameObject FourthDoctor;
    public Animator door;
    public GameObject RotationController;
    // Start is called before the first frame update
    void Start()
    {
        myAnimator = gameObject.GetComponent<Animator>();
        talkedFirstInstructions = false;
        keyGrabbed = false;
        finishedTalking = false;
        checkPositionZ = false;
        checkPositionX = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(checkPositionX){
            if(gameObject.transform.position.x <= positionToCheck){
                checkPositionX = false;
                instructionsAfterKeySecondPart();
            }
        }
        else if(checkPositionZ){
            if(gameObject.transform.position.z >= positionToCheck){
                checkPositionZ = false;
                instructionsAfterKeyThirdPart();
            }
        }
    }

    public void setAnimationTalking(){
        myAnimator.SetBool("GazedAt", true);
    }

    public void setAnimationWalking(){
        myAnimator.SetBool("Walk", true);
    }

    public void setAnimationIdle(){
        myAnimator.SetBool("GazedAt", false);
        myAnimator.SetBool("Walk", false);
    }

    public void OnPointerEnter(){
        coroutine = startTalking();
        StartCoroutine(coroutine);
    }

    private IEnumerator startTalking(){
        if (!talkedFirstInstructions){
            talkedFirstInstructions = true;
            setAnimationTalking();
            instructions.Play();
            yield return new WaitForSeconds(instructions.clip.length);
            setAnimationIdle();
            yield return new WaitForSeconds(4f);
            finishedTalking = true;
        }
        else if(finishedTalking){
            finishedTalking = false;
            if(keyGrabbed)
                instructionsAfterKey();
            else{
                setAnimationTalking();
                keyNotGrabbed.Play();
                yield return new WaitForSeconds(keyNotGrabbed.clip.length - 2);
                setAnimationIdle();
                finishedTalking = true;
            }
        }
    }

    public void instructionsAfterKey(){
        coroutine = instructionsAfterKeyCo();
        StartCoroutine(coroutine);
    }

    private IEnumerator instructionsAfterKeyCo(){
        key.SetActive(false);
        setAnimationTalking();
        instructionsNextRoom.Play();
        yield return new WaitForSeconds(instructionsNextRoom.clip.length);
        RotationController.transform.rotation = Quaternion.Euler(0, 0, 0);
        setAnimationWalking();
        positionToCheck = -20.55f;
        checkPositionX = true;
    }

    private void instructionsAfterKeySecondPart(){
        setAnimationIdle();
        RotationController.transform.position = gameObject.transform.position;
        gameObject.transform.position = RotationController.transform.position;
        RotationController.transform.rotation = Quaternion.Euler(0, 90, 0);
        setAnimationWalking();
        positionToCheck = -2.6f;
        checkPositionZ = true;
    }

    private void instructionsAfterKeyThirdPart(){
        setAnimationIdle();
        RotationController.SetActive(false);
        FourthDoctor.SetActive(true);
        door.SetBool("open", true);
    }

    public void grabKey(){
        UnityEngine.Debug.Log("key grabbed");
        keyGrabbed = true;
    }

    public void ungrabKey(){
        UnityEngine.Debug.Log("key ungrabbed");
        keyGrabbed = false;
    }
}
