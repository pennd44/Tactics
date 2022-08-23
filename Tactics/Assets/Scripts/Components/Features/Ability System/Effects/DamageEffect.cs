using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class DamageEffect : ActionEffect
{
    [SerializeField]private int damage;
    public override void AffectTarget(GameObject target)
    {
            Debug.Log("Inside Affect Target");
            Character unit = target.GetComponent<Character>();
            Health health = unit.GetComponent<Health>();
            unit.unitAnimator.SetTrigger("hit");
            
            health.ReduceCurrent(5);
            if(health.current == 0)
            {
                unit.Die();
            }
    }
    // public int CalculateDamage(Character unit, Character target, ){
        
    //     return damage;
    // }

}
