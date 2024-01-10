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
    [SerializeField] float fadeInTime = .5f;
    [SerializeField] float fadeOutTime = .5f;
    [SerializeField] float fadeWaitTime = .5f;

    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Player")
        {
            StartCoroutine(Transition());        }
    }
    private IEnumerator Transition()
    {
        if (sceneToLoad < 0)
        {
            yield break;
        }
        Fader fader = FindObjectOfType<Fader>();
        yield return StartCoroutine(fader.FadeOut(fadeOutTime));
        DontDestroyOnLoad(gameObject);
        yield return SceneManager.LoadSceneAsync(sceneToLoad);
        print("scene loaded");
        Portal otherPortal = GetOtherPortal();
        UpdatePlayer(otherPortal);
        yield return new WaitForSeconds(fadeWaitTime);
        yield return StartCoroutine(fader.FadeIn(fadeInTime));

        Destroy(gameObject);
    }

    private void UpdatePlayer(Portal otherPortal)
    {
        GameObject player = GameObject.FindWithTag("Player");
        Debug.Log("Player found! noice");
        player.GetComponent<NavMeshAgent>().enabled = false;
        // player.GetComponent<NavMeshAgent>().Warp(otherPortal.spawnPoint.position);
        player.transform.position = otherPortal.spawnPoint.position;
        player.transform.rotation = otherPortal.spawnPoint.rotation;
        player.GetComponent<NavMeshAgent>().enabled = true;
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
