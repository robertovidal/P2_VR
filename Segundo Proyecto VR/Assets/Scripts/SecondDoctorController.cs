using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondDoctorController : MonoBehaviour
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
    public GameObject ThirdDoctor;
    public Animator door;
    public GameObject RotationController;
    public AudioSource DoorSound;
    private Collider MyCollider;

    // Start is called before the first frame update
    void Start()
    {
        myAnimator = gameObject.GetComponent<Animator>();
        MyCollider = gameObject.GetComponent<Collider>();
        talkedFirstInstructions = false;
        keyGrabbed = false;
        finishedTalking = false;
        checkPositionZ = false;
        checkPositionX = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(checkPositionZ){
            if(gameObject.transform.position.z >= positionToCheck){
                checkPositionZ = false;
                instructionsAfterKeySecondPart();
            }
        }
        else if(checkPositionX){
            if(gameObject.transform.position.x <= positionToCheck){
                checkPositionX = false;
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

    private IEnumerator rotate90LeftCo(){
        myAnimator.SetBool("Rotate90Left", true);
        yield return new WaitForSeconds(2f);
        myAnimator.SetBool("Rotate90Left", false);
    }

    public void OnPointerEnter(){
        coroutine = startTalking();
        StartCoroutine(coroutine);
    }

    private IEnumerator startTalking(){
        if (!talkedFirstInstructions){
            talkedFirstInstructions = true;
            gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
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
                gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
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
        gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
        setAnimationTalking();
        instructionsNextRoom.Play();
        yield return new WaitForSeconds(instructionsNextRoom.clip.length);
        MyCollider.enabled = false;
        setAnimationWalking();
        positionToCheck = -5.3f;
        checkPositionZ = true;
    }

    private void instructionsAfterKeySecondPart(){
        setAnimationIdle();
        RotationController.transform.position = gameObject.transform.position;
        gameObject.transform.position = RotationController.transform.position;
        RotationController.transform.rotation = Quaternion.Euler(0, 0, 0);
        gameObject.transform.rotation = Quaternion.Euler(0, 270, 0);
        setAnimationWalking();
        positionToCheck = -10f;
        checkPositionX = true;
    }

    private void instructionsAfterKeyThirdPart(){
        setAnimationIdle();
        RotationController.SetActive(false);
        ThirdDoctor.SetActive(true);
        DoorSound.Play();
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
