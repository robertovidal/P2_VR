using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoctorRotationController : MonoBehaviour
{

    Animator myAnimator;
    private IEnumerator coroutine;
    // Start is called before the first frame update
    void Start()
    {
        myAnimator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void rotate90Left(){
        coroutine = rotate90LeftCo();
        StartCoroutine(coroutine);
    }

    private IEnumerator rotate90LeftCo(){
        myAnimator.SetBool("Rotate90Left", true);
        yield return new WaitForSeconds(1f);
        myAnimator.SetBool("Rotate90Left", false);
    }
}
