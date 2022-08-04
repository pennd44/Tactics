using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability : ScriptableObject
{
    public new string name;
    public int staminaCost;
    public int kiCost;
    public virtual void Use(GameObject parent){}
    public virtual void ShowSelectableTiles(){}
}
