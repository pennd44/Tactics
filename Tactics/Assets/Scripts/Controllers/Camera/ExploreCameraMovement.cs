using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExploreCameraMovement : MonoBehaviour
{
    Transform partyLeader;
    private void OnEnable()
    {
        // Debug.Log("ExploreCameraMovement OnEnable");
        partyLeader = GameObject.FindObjectOfType<Player>().transform;
    }
    void Update()
    {
        transform.position = partyLeader.position;
    }
}
