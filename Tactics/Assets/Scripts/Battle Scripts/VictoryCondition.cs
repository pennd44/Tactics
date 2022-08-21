using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class VictoryCondition
{
    public Alliances victor;
    public BattleStateMachine battleStateMachine;
    // private void Awake(){
    //     battleStateMachine = GetComponent<BattleStateMachine>();
    // }
    protected virtual bool PartyDefeated(Alliances type)
    {
        Debug.Log("Alliance " + type);
        Debug.Log("bsm c c " + battleStateMachine.characters.Count);
        for (int i = 0; i < battleStateMachine.characters.Count; ++i)
        {
            Alliance a = battleStateMachine.characters[i].GetComponent<Alliance>();
            if (a == null)
                continue;
            if (a.type == type && !battleStateMachine.characters[i].isDead)
                return false;
        }
        return true;
    }
    public virtual void CheckForGameOver ()
    {
        if (PartyDefeated(Alliances.Hero))
            victor = Alliances.Enemy;
    }
}
