using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float speed = 10;
    Tile target = null;
    void Update()
    {
        if (target == null) return;
        transform.LookAt(GetAimLocation());
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
    public void SetTarget(Tile target){
        this.target = target;
    }
    private Vector3 GetAimLocation(){
        if(target.content != null){
            return target.content.transform.position +  Vector3.up * 1;
        }
        // if (targetCapsule == null)
        // {}
            return target.transform.position;
        // }
        // return target.transform.position + Vector3.up * targetCapsule.height/2;
    }
    private void OnTriggerEnter(Collider other) {
        //make it so if arrow hits tile it dissapears.  Will use terrain collider and check if other = board
        if(other.gameObject != target.transform.parent.gameObject && other.gameObject != target.content) return;
        if(other.gameObject == target.content){
            target.content.GetComponent<Character>().GetHit();
            // OnArrowHit(target.content);
        }
        Destroy(gameObject);
    }


    ///Testing Events
    // public delegate void ArrowHit(GameObject target);
    // public static event ArrowHit OnArrowHit;


}
