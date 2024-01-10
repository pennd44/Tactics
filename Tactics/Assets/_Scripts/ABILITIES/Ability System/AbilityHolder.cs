using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityHolder : MonoBehaviour
{
    public Ability ability;
    private Character unit;
    private void Awake() {
        unit = GetComponent<Character>();
    }
    public Projectile projectile = null;
    public void ChangeAbility(Ability abi){
        // Debug.Log("Changing ability");
        ability = abi;
        ability.unit = unit;
        // Debug.Log(ability.unit);
        if(abi.HasProjectile()){
            projectile = abi.projectile;
        }
        ability.OnSelectAbility();
    }
    // enum AbilityState{available, active, unavailable}
    // AbilityState state = AbilityState.available;
}
