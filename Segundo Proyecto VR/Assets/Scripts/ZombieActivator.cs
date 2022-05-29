using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieActivator : MonoBehaviour
{
    private bool activated;
    public Animator door;
    public Animator zombie;
    public AudioSource zombieAudio;
    public AudioSource zombieScream;
    private IEnumerator coroutine;
    public Light light1;
    public Light light2;
    public Light light3; 
    public GameObject ZombieController; 
    public GameObject zombieGameObject; 
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
        if((other.gameObject.tag == "Player") && !activated){
            activated = true;
            door.SetBool("close", true);
            turnLightsOn();
            coroutine = zombieAnimation();
            StartCoroutine(coroutine);
        }
    }

    private IEnumerator zombieAnimation(){
        zombieAudio.Play();
        yield return new WaitForSeconds(zombieAudio.clip.length);
        zombie.SetBool("run", true);
        yield return new WaitForSeconds(0.7f);
        zombieGameObject.transform.position = new Vector3(-26.904583f,0f,-3.95799994f);
        zombie.SetBool("attack", true);
        zombie.SetBool("run", false);
        zombieScream.Play();
    }

    private void turnLightsOn(){
        light1.enabled = true;
        light2.enabled = true;
        light3.enabled = true;
    }
}
