using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniCam : MonoBehaviour
{

    public Transform player;
    Vector3 originalDistance;
        
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Transform>();
        originalDistance = this.transform.position - player.position;
    }
        
    void Update()
    {
        transform.position = player.position + originalDistance;
        transform.rotation = player.rotation * Quaternion.Euler(90, 0, 0);
    }
}
