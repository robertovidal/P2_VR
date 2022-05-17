using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoctorController : MonoBehaviour
{
    public Animator myAnimator;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setAnimationTalking(){
        myAnimator.SetBool("GazedAt", true);
    }

    public void setAnimationIdle(){
        myAnimator.SetBool("GazedAt", false);
    }
}
