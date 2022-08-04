using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class BattleCameraMovement : MonoBehaviour
{
    private float moveSpeed = 10f; 
    private float rotationSpeed = 3.5f;
    private float zoomAmount = 1f;


    private const float MIN_FOLLOW_Y_OFFSET = 2f;
    private const float MAX_FOLLOW_Y_OFFSET = 12f;
    [SerializeField] private CinemachineVirtualCamera cinemachineVirtualCamera;
    private CinemachineTransposer cinemachineTransposer; 
    private void Awake() {
        cinemachineTransposer = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineTransposer>();
    }
    private void Update() {
        Vector3 inputMoveDir = new Vector3(0,0,0);
        if(Input.GetKey(KeyCode.W))
        {
            inputMoveDir.z = +1f;
        }
        if(Input.GetKey(KeyCode.S))
        {
            inputMoveDir.z = -1f;
        }
        if(Input.GetKey(KeyCode.A))
        {
            inputMoveDir.x = -1f;
        }
        if(Input.GetKey(KeyCode.D))
        {
            inputMoveDir.x = +1f;
        }
        Vector3 moveVector = transform.forward * inputMoveDir.z + transform.right * inputMoveDir.x;
        transform.position += moveVector * moveSpeed * Time.deltaTime;
        
        Vector3 rotationVector = new Vector3(0,0,0);
        mouseLook();
        zoom();
    }

    private void mouseLook(){
        float Y;
        // float X;
        if(Input.GetMouseButton(1)) 
        {
             transform.Rotate(new Vector3(Input.GetAxis("Mouse Y") * rotationSpeed, -Input.GetAxis("Mouse X") * rotationSpeed, 0));
            //  X = transform.rotation.eulerAngles.x;
             Y = transform.rotation.eulerAngles.y;
             transform.rotation = Quaternion.Euler(0, Y, 0);
        }
    }
    private void zoom(){
        Vector3 followOffset = cinemachineTransposer.m_FollowOffset;
        if(Input.mouseScrollDelta.y > 0)
        {
            followOffset.y -= zoomAmount;
            // followOffset.z -= zoomAmount;
        }
        if(Input.mouseScrollDelta.y < 0)
        {
            followOffset.y += zoomAmount;
            // followOffset.z += zoomAmount;
        }
        followOffset.y = Mathf.Clamp(followOffset.y, MIN_FOLLOW_Y_OFFSET, MAX_FOLLOW_Y_OFFSET);
        cinemachineTransposer.m_FollowOffset = followOffset;
    }
}
