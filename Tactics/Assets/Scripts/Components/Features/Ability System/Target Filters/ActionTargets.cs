using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class ActionTargets
{
    public virtual bool CheckHit(Tile tile){
        if(tile.content.GetComponent<Character>() != null){
            return true;
        }
        return false;
    }
}
