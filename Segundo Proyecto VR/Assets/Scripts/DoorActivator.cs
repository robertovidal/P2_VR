using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorActivator : MonoBehaviour
{
    private bool activated;
    public Animator door;
    public GameObject key;
    public AudioSource DoorSound;
    // Start is called before the first frame update
    void Start()
    {
        activated = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        if((other.gameObject.tag == "Key") && !activated){
            activated = true;
            door.SetBool("open", true);
            DoorSound.Play();
            key.SetActive(false);
        }
    }
}
