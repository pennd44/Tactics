using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battler : MonoBehaviour
{
    private BattleStateMachine battleStateMachine;
    public BattleMovementStateMachine mover;
    public AbilityHolder abilityHolder;
    public Alliance alliance;
    // private void Awake() {
    //     alliance = GetComponent<Alliance>();
    //     Stat newStat; 
    //     foreach (var val in Enum.GetValues(typeof(Stats)))
    //     {
    //         newStat = new Stat((Stats)val);
    //         stats.Add(newStat);
    //     }
    //     battleStateMachine = GameObject.FindObjectOfType<BattleStateMachine>();
    //     mover = GetComponent<BattleMovementStateMachine>();
    //     equipmentLoadout = GetComponent<EquipmentLoadout>();
    // }
}