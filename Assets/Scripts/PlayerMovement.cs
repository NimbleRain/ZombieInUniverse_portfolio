using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    private float moveSpeed = 5f;
    private float rotateSpeed = 180f;

    private PlayerInput plInput; 
    private Rigidbody plRid;     
    private Animator plAnim;

    private Camera miniCamera;
        
    void Start()
    {
        plInput = GetComponent<PlayerInput>();
        plRid = GetComponent<Rigidbody>();
        plAnim = GetComponent<Animator>();
        miniCamera = GetComponent<Camera>();
    }

    private void FixedUpdate()
    {
        Rotate();
        Move();

        plAnim.SetFloat("Move", plInput.move);
    } 
    void Move()
    {
        Vector3 moveDistance = plInput.move * transform.forward 
                               * moveSpeed * Time.deltaTime;
        plRid.MovePosition(plRid.position + moveDistance);
    }

    void Rotate()
    {
        float turn = plInput.rotate * rotateSpeed * Time.deltaTime;
        plRid.rotation = plRid.rotation * Quaternion.Euler(0, turn, 0f);
    }
}
