using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class DamageEffect : ActionEffect
{
    [SerializeField]private int damage;
    public override void AffectTarget(GameObject target)
    {
       
            Character unit = target.GetComponent<Character>();
            Health health = unit.GetComponent<Health>();
            
            health.ReduceCurrent(damage);
            if(health.current == 0)
            {
                unit.Die();
            }
    }
    // public int CalculateDamage(Character unit, Character target, ){
        
    //     return damage;
    // }

}
