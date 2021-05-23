using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    private Animator anim;
    public AudioClip doorOpenAudio;
    public AudioClip doorCloseAudio;
    private AudioSource doorSound;    
    void Start()
    {
        anim = GetComponent<Animator>();
        doorSound = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            anim.SetBool("isOpen", true);
            doorSound.PlayOneShot(doorOpenAudio);            
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            anim.SetBool("isOpen", false);
            doorSound.PlayOneShot(doorCloseAudio);    
        }
    }
}
