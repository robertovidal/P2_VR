using System.Diagnostics;
using System.Runtime.Serialization.Formatters;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoctorController : MonoBehaviour
{
    public Animator myAnimator;

    private bool talked;
    public AudioSource instruccionesIniciales;
    private IEnumerator coroutine;

    public GameObject RotationController;
    public GameObject SecondDoctor;
    private bool checkPosition;
    private float positionToCheck;
    private Collider MyCollider;
    // Start is called before the first frame update
    void Start()
    {
        talked = false;
        checkPosition = false;
        MyCollider = gameObject.GetComponent<Collider>();
        if(PlayerPrefs.HasKey("volume")){
            float wantedVolume = PlayerPrefs.GetFloat("volume",1f);
            UnityEngine.Debug.Log(wantedVolume);
            AudioListener.volume = wantedVolume;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(checkPosition){
            if(gameObject.transform.position.x >= positionToCheck){
                UnityEngine.Debug.Log(gameObject.transform.position.x);
                secondPart();
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

    public void rotate90Left(){
        coroutine = rotate90LeftCo();
        StartCoroutine(coroutine);
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
            gameObject.transform.rotation = Quaternion.Euler(0, 180, 0);
            setAnimationTalking();
            instruccionesIniciales.Play();
            yield return new WaitForSeconds(instruccionesIniciales.clip.length - 2);
            MyCollider.enabled = false;
            gameObject.transform.rotation = Quaternion.Euler(0, 90, 0);
            setAnimationWalking();
            positionToCheck = 19.5f;
            checkPosition = true;
        }
    }

    private void secondPart(){
        coroutine = secondPartCo();
        StartCoroutine(coroutine);
    }

    private IEnumerator secondPartCo(){
        setAnimationIdle();
        RotationController.transform.position = gameObject.transform.position;
        gameObject.transform.position = RotationController.transform.position;
        gameObject.transform.rotation = Quaternion.Euler(0, 180, 0);
        setAnimationWalking();
        yield return new WaitForSeconds(2f);
        setAnimationIdle();
        RotationController.SetActive(false);
        SecondDoctor.SetActive(true);
    }
}
