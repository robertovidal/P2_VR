using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FourthDoctorController : MonoBehaviour
{
    private Animator myAnimator;
    private bool talkedFirstInstructions;
    public AudioSource instructions;
    public AudioSource keyNotGrabbed;
    private IEnumerator coroutine;
    private bool finishedTalking;
    // Start is called before the first frame update
    void Start()
    {
        myAnimator = gameObject.GetComponent<Animator>();
        setAnimationScared();
         talkedFirstInstructions = false;
         finishedTalking = false;
    }

    // Update is called once per frame
    void Update()
    {
        
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

    public void setAnimationScared(){
        myAnimator.SetBool("Scared", true);
    }

    public void OnPointerEnter(){
        coroutine = startTalking();
        StartCoroutine(coroutine);
    }

    private IEnumerator startTalking(){
        if (!talkedFirstInstructions){
            talkedFirstInstructions = true;
            instructions.Play();
            yield return new WaitForSeconds(instructions.clip.length);
            yield return new WaitForSeconds(4f);
            finishedTalking = true;
        }
        else if(finishedTalking){
            finishedTalking = false;
            keyNotGrabbed.Play();
            yield return new WaitForSeconds(keyNotGrabbed.clip.length);
            finishedTalking = true;
        }
    }
}
