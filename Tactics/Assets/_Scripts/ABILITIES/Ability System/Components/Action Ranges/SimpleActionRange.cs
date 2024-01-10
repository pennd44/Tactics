using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// [System.Serializable]
[CreateAssetMenu(fileName = "SimpleActionRange", menuName = "Abilities/Ability Components/ActionRange/SimpleActionRange", order = 0)]
public class SimpleActionRange : ActionRange
{
    // public SimpleActionRange(int hor, int vert) : base(hor,vert){}
    protected override bool ExpandSearch (Tile from, Tile to)
    {
        if((Mathf.Abs(from.height - to.height) > vertical))
            return false;
        return base.ExpandSearch(from, to);
    }
}