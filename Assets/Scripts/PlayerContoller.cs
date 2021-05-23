using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerContoller : MonoBehaviour
{

    [SerializeField] private Transform character;
    [SerializeField] private Transform CenterAxis;   
    
    private float rotationX = 0.0f;
    private float rotationY = 0.0f;

    private const float clampAngleDegrees = 80.0f;
    private const float sensitivity = 2.0f;

    void Start()
    {        
        Vector3 rotation = CenterAxis.transform.localRotation.eulerAngles;
        rotationX = rotation.x;
        rotationY = rotation.y;
    }
    void Update()
    {
        //if (Input.GetMouseButtonDown(0))
        //{
        //    SetCursorLock(true);
        //}
        //else if (Input.GetKeyDown(KeyCode.Escape))
        //{
        //    SetCursorLock(false);
        //}

        Move();
        LookAround();        
    }

    private void Move() //카메라의 이동에 따른 수평을 맞추기 위해
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        rotationX += sensitivity * mouseY;
        rotationY += sensitivity * mouseX;
        rotationX = Mathf.Clamp(rotationX, -clampAngleDegrees, clampAngleDegrees);
        CenterAxis.transform.localRotation = Quaternion.Euler(rotationX, rotationY, 0.0f);
                
        Debug.DrawRay(CenterAxis.position, new Vector3(CenterAxis.forward.x, 
                      0f, CenterAxis.forward.z).normalized, Color.red);
    }

    private void LookAround()
    {
        Vector2 mouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        Vector3 camAngle = CenterAxis.rotation.eulerAngles;
        //상하방향 카메라 회전의 제한
        float x = camAngle.x - mouseDelta.y;

        if(x < 180f) //180도 보다 작은 위쪽에서 회전하는 경우
        {
            x = Mathf.Clamp(x, -1f, 70f); //0도면 카메라가 수평면 아래로 가지 않음
        }
        else //아래쪽
        {
            x = Mathf.Clamp(x, 335f, 361f);
        }
        CenterAxis.rotation = Quaternion.Euler(x, camAngle.y + mouseDelta.x, camAngle.z);
    }

    //private void SetCursorLock(bool lockCursor) //게임창 마우스 안나가게
    //{
    //    if (lockCursor)
    //    {
    //        Cursor.lockState = CursorLockMode.Locked;
    //        Cursor.visible = false;
    //    }
    //    else
    //    {
    //        Cursor.lockState = CursorLockMode.None;
    //        Cursor.visible = true;
    //    }
    //}
//-----------------------------------------------------------------------------------------------//
    //void Update()
    //{
    //    float moveX = Input.GetAxis("Horizontal");
    //    float moveZ = Input.GetAxis("Vertical");

    //    Vector3 moveVec = new Vector3(moveX, 0f, moveZ);

    //    bool isMove = moveVec.magnitude > 0;
    //    //animator.SetBool("isMove", isMove);
    //    LookMouseCursor();

    //    transform.Translate(new Vector3(moveX, 0f, moveZ).normalized * Time.deltaTime * 5f);
    //}

    //public void LookMouseCursor() //스페이스 공간상의 좌표에서 3D공간상의 카메라좌표
    //{
    //    Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
    //    RaycastHit hit;
    //    if(Physics.Raycast(ray, out hit))
    //    {
    //        Vector3 mouseDir = new Vector3(hit.point.x, transform.position.y, hit.point.z) - transform.position;
    //        //animator.transform.forward = mouseDir;
    //    }
    //}
}
