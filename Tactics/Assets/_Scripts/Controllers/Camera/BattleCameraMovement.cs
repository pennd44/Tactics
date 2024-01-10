using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class BattleCameraMovement : MonoBehaviour
{
    private float moveSpeed = 10f; 
    private float rotationSpeed = 3.5f;
    private float zoomAmount = 1f;
    private float zoomSpeed = 5f;


    private const float MIN_FOLLOW_Y_OFFSET = 2f;
    private const float MAX_FOLLOW_Y_OFFSET = 12f;
    [SerializeField] private CinemachineVirtualCamera cinemachineVirtualCamera;
    private Vector3 targetFollowOffset;
    private CinemachineTransposer cinemachineTransposer; 
    private void Awake() {
        cinemachineTransposer = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineTransposer>();
        targetFollowOffset = cinemachineTransposer.m_FollowOffset;
    }
    private void Update() {
        HandleMovement();
        HandleRotation();
        HandleZoom();
    }

    private void HandleRotation(){
        float Y;
        // float X;
        if(Input.GetMouseButton(1)) 
        {
             transform.Rotate(new Vector3(Input.GetAxis("Mouse Y") * rotationSpeed, Input.GetAxis("Mouse X") * rotationSpeed, 0));
            //  X = transform.rotation.eulerAngles.x;
             Y = transform.rotation.eulerAngles.y;
             transform.rotation = Quaternion.Euler(0, Y, 0);
        }
    }
    private void HandleMovement(){
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
    }
    private void HandleZoom(){
        
        if(Input.mouseScrollDelta.y > 0)
        {
            targetFollowOffset.y -= zoomAmount;
            // followOffset.z -= zoomAmount;
        }
        if(Input.mouseScrollDelta.y < 0)
        {
            targetFollowOffset.y += zoomAmount;
            // followOffset.z += zoomAmount;
        }
        targetFollowOffset.y = Mathf.Clamp(targetFollowOffset.y, MIN_FOLLOW_Y_OFFSET, MAX_FOLLOW_Y_OFFSET);
        cinemachineTransposer.m_FollowOffset = Vector3.Lerp(cinemachineTransposer.m_FollowOffset, targetFollowOffset, Time.deltaTime* zoomSpeed);
    }
}
