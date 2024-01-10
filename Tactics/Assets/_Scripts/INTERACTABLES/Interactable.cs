using System.Collections.Generic;
using UnityEngine;
public abstract class Interactable : MonoBehaviour
{
    [SerializeField] public float radius = 2f;
    private Character player;    
    public virtual void ApplyEffects(Character character){}

    public virtual bool CanInteract(Character character){
        return true;
    }
    // private void OnTriggerEnter(Collider other) {
    //     if(other.gameObject.tag == "Player"){
    //         player = other.GetComponent<Character>();
    //     }
    // }
    // //Is this needed?
    // // private void OnTriggerExit(Collider other) {
    // //     if(other.gameObject.tag == "Player"){
    // //         player = null;
    // //     }
    // // }
    // protected void OnTriggerStay(Collider other) {
    //     if(CanInteract(player)){
    //         if(Input.GetKeyDown(KeyCode.F)){
    //             ApplyEffects(player);
    //         }
    //     }
    // }
    void OnDrawGizmosSelected() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}