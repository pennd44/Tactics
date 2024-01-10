using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AbilityCreatingInteractable : Interactable 
{
    [SerializeField] AnimatorOverrideController animatorOverride;
    [SerializeField] public Projectile projectile = null;
    public new string name;
    [SerializeField] public int healthCost = 0;
    [SerializeField] public int staminaCost;
    [SerializeField] public int kiCost;

    [SerializeField] ActionRange actionRange;
    [SerializeField] public int rangeHorizontal;
    [SerializeField] public int rangeVertical;
    [SerializeField] public ActionArea areaOfEffect;
    [SerializeField] public int AOEHorizontal;
    [SerializeField] public int AOEVertical;
    [SerializeField] ActionTargets targetFilter;
    [SerializeField] List<ActionEffect> effects = new List<ActionEffect>();
    [SerializeField] public int [] effectAmmounts;
    [SerializeField] bool isRightHanded = true;
    public override void ApplyEffects(Character character){
        Ability newAbility = ScriptableObject.CreateInstance<Ability>();
        newAbility.Init(animatorOverride, name, healthCost, staminaCost, kiCost, actionRange, rangeHorizontal, rangeVertical, areaOfEffect, AOEHorizontal, AOEVertical, targetFilter, effects, effectAmmounts, isRightHanded, projectile);
        Debug.Log("created " + newAbility.name);
        // AssetDatabase.CreateAsset(newAbility, "Assets/" + newAbility.name);  do i need to save it?
        character.AquireSkill(newAbility);
    }
    public override bool CanInteract(Character character){
        return true;
    }
    void OnDrawGizmosSelected() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);    
    }
}