using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefeatAllEnemiesCondition : VictoryCondition
{
    public override void CheckForGameOver ()
    {
        base.CheckForGameOver();
        if (victor == Alliances.None && PartyDefeated(Alliances.Enemy))
        victor = Alliances.Hero;
    }
}
