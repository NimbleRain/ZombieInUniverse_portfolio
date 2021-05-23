using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuccessLayer : MonoBehaviour
{
    public AudioClip successAudio;
    private AudioSource successSound;

    void Start()
    {
        successSound = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            successSound.PlayOneShot(successAudio);
            UIManager.Instance.gameSuccessUI.SetActive(true);            
        }

    }
}
