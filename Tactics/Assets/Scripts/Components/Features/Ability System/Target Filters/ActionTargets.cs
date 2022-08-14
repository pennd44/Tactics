using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionTargets : MonoBehaviour
{
    public virtual bool CheckHit(Tile tile){
        if(tile.content.GetComponent<Character>() != null){
            return true;
        }
        return false;
    }
}
