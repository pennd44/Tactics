using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class SimpleActionRange : ActionRange
{
    public SimpleActionRange(int hor, int vert) : base(hor,vert){}
    protected override bool ExpandSearch (Tile from, Tile to)
    {
        if((Mathf.Abs(from.height - to.height) > unit.jumpHeight))
            return false;
        return base.ExpandSearch(from, to);
    }
}