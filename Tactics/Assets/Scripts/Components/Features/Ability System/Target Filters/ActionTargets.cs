using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class ActionTargets
{
    public virtual bool CheckHit(Tile tile){
                Debug.Log("hit Check hit");
        if(tile.content.GetComponent<Character>() != null){
            return true;
        }
        return false;
    }
}
