using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExploreCameraMovement : MonoBehaviour
{
    Transform partyLeader;
    void Start()
    {
        partyLeader = GameObject.FindObjectOfType<Player>().transform;
    }
    void Update()
    {
        transform.position = partyLeader.position;
    }
}
