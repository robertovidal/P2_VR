using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalDoorController : MonoBehaviour
{
    private bool opened;
    public Animator door;
    private bool keyGrabbed;
    public GameObject key;
    public AudioSource DoorSound;
    // Start is called before the first frame update
    void Start()
    {
        opened = false;
        keyGrabbed = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void grabKey(){
        UnityEngine.Debug.Log("key grabbed");
        keyGrabbed = true;
    }

    public void ungrabKey(){
        UnityEngine.Debug.Log("key ungrabbed");
        keyGrabbed = false;
    }

    private void OnPointerEnterCloser() {
        if(keyGrabbed && !opened){
            opened = true;
            door.SetBool("open", true);
            DoorSound.Play();
            key.SetActive(false);
        }
    }

}
