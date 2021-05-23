using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    //플레이어 입력 감지 스크립트
    public string moveAxisName = "Vertical";
    public string rotateAxisName = "Horizontal";
    public string fireButtonName = "Fire1";
    public string reloadButtonName = "Reload";       

    //감지 입력값
    public float move { get; private set; }
    public float rotate { get; private set; }
    public bool fire { get; private set; }
    public bool reload { get; private set; }   

    void Update()
    { 
        //게임이 끝났을 때 감지 하지 않음
        if (GameManager.Instance != null && GameManager.Instance.isGameover)
        {
            move = 0;
            rotate = 0;
            fire = false;
            reload = false;
            return;
        }
        move = Input.GetAxisRaw(moveAxisName);
        rotate = Input.GetAxisRaw(rotateAxisName);
        fire = Input.GetButton(fireButtonName);
        reload = Input.GetButtonDown(reloadButtonName);           
        }    
}
