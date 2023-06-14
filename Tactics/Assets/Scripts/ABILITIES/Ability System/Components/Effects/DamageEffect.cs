using UnityEngine;
// [System.Serializable]
[CreateAssetMenu(fileName = "DamageEffect", menuName = "Abilities/Ability Components/ActionEffect/DamageEffect", order = 0)]

public class DamageEffect : ActionEffect
{
    // [SerializeField]private int damage;
    public override void AffectTarget(GameObject target, int damage)
    {
            Debug.Log("Inside Affect Target");
            Character unit = target.GetComponent<Character>();
            Health health = unit.GetComponent<Health>();
            // unit.unitAnimator.SetTrigger("hit");
            
            health.ReduceCurrent(damage);
            // unit.damageAmount = damage;
            unit.GetHit();
            DamagePopup.Create(unit.transform.position, damage, false);
            if(health.current == 0)
            {
                unit.Die();
            }
            unit.OnGetHit();
    }
    // public int CalculateDamage(Character unit, Character target, ){
        
    //     return damage;
    // }

}
