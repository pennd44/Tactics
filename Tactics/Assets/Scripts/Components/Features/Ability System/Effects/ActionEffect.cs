using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class ActionEffect
{
    public Character unit;
    public abstract void AffectTarget(GameObject target);
}