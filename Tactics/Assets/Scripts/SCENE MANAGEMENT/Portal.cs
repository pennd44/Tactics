using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;
public class Portal : MonoBehaviour
{
    enum DestinationIdentifier
    {
        A, B, C, D, E
    }
    [SerializeField] int sceneToLoad = -1;
    [SerializeField] Transform spawnPoint;
    [SerializeField] DestinationIdentifier destination;
    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Player")
        {
            StartCoroutine(Transition());        }
    }
    private IEnumerator Transition()
    {
        DontDestroyOnLoad(gameObject);
        yield return SceneManager.LoadSceneAsync(sceneToLoad);
        print("scene loaded");
        Portal otherPortal = GetOtherPortal();
        UpdatePlayer(otherPortal);

        Destroy(gameObject);
    }

    private void UpdatePlayer(Portal otherPortal)
    {
        GameObject player = GameObject.FindWithTag("Player");
        Debug.Log("Player found! noice");
        // player.GetComponent<NavMeshAgent>().enabled = false;
        player.GetComponent<NavMeshAgent>().Warp(otherPortal.spawnPoint.position);
        player.transform.position = otherPortal.spawnPoint.position;
        player.transform.rotation = otherPortal.spawnPoint.rotation;
        // player.GetComponent<NavMeshAgent>().enabled = true;
    }

    private Portal GetOtherPortal()
    {
        foreach (Portal portal in FindObjectsOfType<Portal>())
        {
            if (portal == this) continue;
            if (portal.destination != destination) continue;
            Debug.Log("Portal found!");
            return portal;
        }
        Debug.Log("No Portal found :/");
       return null;
    }
}
