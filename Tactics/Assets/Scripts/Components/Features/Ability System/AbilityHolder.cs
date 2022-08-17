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
    public void ChangeAbility(Ability abi){
        ability = abi;
        ability.unit = unit;
        ability.OnSelectAbility();
    }
    // enum AbilityState{available, active, unavailable}
    // AbilityState state = AbilityState.available;
}
