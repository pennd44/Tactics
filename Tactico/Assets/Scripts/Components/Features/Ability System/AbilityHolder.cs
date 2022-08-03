using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityHolder : MonoBehaviour
{
    public Ability ability;
    
    enum AbilityState{available, active, unavailable}
    AbilityState state = AbilityState.available;
}
