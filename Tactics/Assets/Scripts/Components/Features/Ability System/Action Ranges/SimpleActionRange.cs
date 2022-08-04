using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SimpleActionRange : ActionRange
{
    protected override bool ExpandSearch (Tile from, Tile to)
    {
        if((Mathf.Abs(from.height - to.height) > unit.jumpHeight))
            return false;
        return base.ExpandSearch(from, to);
    }
}