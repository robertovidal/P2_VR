using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doctor2Controller : MonoBehaviour
{
    private Animator myAnimator;
    private bool talked;
    public AudioSource instructions;
    private IEnumerator coroutine;

    // Start is called before the first frame update
    void Start()
    {
        myAnimator = gameObject.GetComponent<Animator>();
        talked = false;
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
        if (!talked){
            talked = true;
            setAnimationTalking();
            instructions.Play();
            yield return new WaitForSeconds(instructions.clip.length);
            setAnimationIdle();
        }
    }
}
