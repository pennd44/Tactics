using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability : ScriptableObject
{
    public new string name;
    public int healthCost = 0;
    public int staminaCost;
    public int kiCost;
    ActionArea areaOfEffect;
    ActionRange actionRange;
    List<ActionEffect> effects;
    ActionTargets targetFilter;
    // ActionCost?
    public virtual void Use(GameObject parent){}
    public virtual void ShowSelectableTiles(){}
}
