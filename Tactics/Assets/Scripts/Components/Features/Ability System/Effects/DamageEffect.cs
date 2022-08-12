using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageEffect : ActionEffect
{
    [SerializeField]private int damage;
    public override void AffectTargets(List<Character> targets)
    {
        for (int i = 0; i < targets.Count; i++)
        {
            Health health = targets[i].GetComponent<Health>();
            
            health.ReduceCurrent(damage);
        }
    }
    // public int CalculateDamage(Character unit, Character target, ){
        
    //     return damage;
    // }

}
