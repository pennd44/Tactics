using UnityEngine;
using System.Collections;

public class FaceTrackMovement : MonoBehaviour {

    [SerializeField]
    UDPReceive receiver = null;
    [SerializeField]
    Transform target;

    Transform startPos = null;

    [SerializeField]
    float reductionVerticalPositionFactor = 1;

    [SerializeField]
    float reductionHorizontalPositionFactor = 1;

    [SerializeField]
    float reductionRotationFactor = 1;


    void Start ()
    {
        startPos = transform;
	}
	
	void Update ()
    {
        transform.position = new Vector3(receiver.xPos * reductionHorizontalPositionFactor, receiver.yPos * reductionVerticalPositionFactor, -receiver.zPos * reductionHorizontalPositionFactor );
        // transform.rotation = Quaternion.Euler(-receiver.pitch * reductionRotationFactor, -receiver.yaw * reductionRotationFactor, receiver.roll * reductionRotationFactor);
        // transform.LookAt(target);
    }
}