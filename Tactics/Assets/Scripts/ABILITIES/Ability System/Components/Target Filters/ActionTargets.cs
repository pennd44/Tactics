using UnityEngine;

// [System.Serializable]
public abstract class ActionTargets : ScriptableObject {

    public virtual bool CheckHit(Tile tile){
        if(tile.content == null)
            return false;
        if(tile.content.GetComponent<Character>() != null){
            return true;
        }
        return false;
    }
}
