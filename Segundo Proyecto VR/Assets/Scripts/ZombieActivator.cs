using System.Net.NetworkInformation;
using System.ComponentModel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ZombieActivator : MonoBehaviour
{
    private bool activated;
    public Animator door;
    public Animator zombie;
    public AudioSource zombieAudio;
    public AudioSource zombieScream;
    public AudioSource violinSound;
    public AudioSource music;
    public AudioSource suspenseMusic;
    private IEnumerator coroutine;
    public Light light1;
    public Light light2;
    public Light light3; 
    public GameObject zombieGameObject; 
    public GameObject mainCamera; 
    public GameObject bloodOverlay; 
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
            violinSound.Play();
            music.Stop();
            suspenseMusic.Play();
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
        zombieGameObject.transform.position = new Vector3(mainCamera.transform.position.x,0f, mainCamera.transform.position.z-1.227f);
        zombie.SetBool("attack", true);
        zombie.SetBool("run", false);
        zombieScream.Play();
        bloodOverlay.SetActive(true);
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(2);
    }

    private void turnLightsOn(){
        light1.enabled = true;
        light2.enabled = true;
        light3.enabled = true;
    }
}
