using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class DamageEffect : ActionEffect
{
    [SerializeField]private int damage;
    public override void AffectTarget(GameObject target)
    {
               Debug.Log("hit affect target");

            Character unit = target.GetComponent<Character>();
            Health health = unit.GetComponent<Health>();
            
            health.ReduceCurrent(5);
               Debug.Log(health.current);
            if(health.current == 0)
            {
               Debug.Log("h0h");
                unit.Die();
            }
    }
    // public int CalculateDamage(Character unit, Character target, ){
        
    //     return damage;
    // }

}
