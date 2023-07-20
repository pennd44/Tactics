using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Ability", menuName = "Abilities/New Ability", order = 0)]
public class Ability : ScriptableObject
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


    // [SerializeField] public Material greenBeen;
    public Character unit;
    // ActionCost?
    public void OnSelectAbility(){
        actionRange.unit = unit;

        for (int i = 0; i < effects.Count; i++)
        {
            effects[i].user = unit;
        }
    }
    public virtual List<Tile> GetSelectableTiles(Board board){
        // actionRange.unit = unit;
        actionRange.SetWidthAndHeight(rangeHorizontal, rangeVertical);
        return actionRange.GetTilesInRange(board);
    }
    public virtual List<Tile> GetTilesInAOE(Board board, Tile tile){
        areaOfEffect.SetWidthAndHeight(AOEHorizontal, AOEVertical);
        return areaOfEffect.GetTilesInArea(board, tile);
    }
    public virtual void Use(List<Tile> tiles){
        for (int i = 0; i < tiles.Count; i++)
        {
            if(!targetFilter.CheckHit(tiles[i]))
            {
                continue;
            }
            for (int j = 0; j < effects.Count; j++)
            {
                effects[j].AffectTarget(tiles[i].content, effectAmmounts[j]);
            }
        }
    }

    //Add animation event here????
    

    public void OverrideAnimation(Animator animator)
    {
        var overrideController = animator.runtimeAnimatorController as AnimatorOverrideController;
        if(animatorOverride != null){
            animator.runtimeAnimatorController = animatorOverride;
        } else if (overrideController != null){
                animator.runtimeAnimatorController = overrideController.runtimeAnimatorController;
        }
    }
    public bool HasProjectile(){
        return projectile != null;
    }
    public void LaunchProjectile(Transform rightHand, Transform leftHand, Tile target){
        Projectile projectileInstance = Instantiate(projectile, GetTransform(rightHand, leftHand).position, Quaternion.identity);
        projectileInstance.SetTarget(target);
    }
    //Debug Method
    public void LogComponents(){
        Debug.Log(actionRange);
        Debug.Log(areaOfEffect);
        Debug.Log(targetFilter);
        Debug.Log(effects);
    }
    public Transform GetTransform(Transform rightHand, Transform leftHand)
    {
        Transform handTransform;
        if (isRightHanded) handTransform = rightHand;
        else handTransform = leftHand;
        return handTransform;
    }


    //Init for use with SKill builder
    public void Init(AnimatorOverrideController animatorOverride, string name, int healthCost, int staminaCost, int kiCost, ActionRange actionRange, int rangeHorizontal, int rangeVertical, ActionArea areaOfEffect, int AOEHorizontal, int AOEVertical, ActionTargets targetFilter, List<ActionEffect> effects, int [] effectAmmounts, bool isRightHanded = true, Projectile projectile = null){
        this.animatorOverride = animatorOverride;
        this.projectile = projectile;
        this.name = name;
        this.healthCost = healthCost;
        this.staminaCost = staminaCost;
        this.kiCost = kiCost;
        this.actionRange = actionRange;
        this.rangeHorizontal = rangeHorizontal;
        this.rangeVertical = rangeVertical;
        this.areaOfEffect = areaOfEffect;
        this.AOEHorizontal = AOEHorizontal;
        this.AOEVertical = AOEVertical;
        this.targetFilter = targetFilter;
        this.effects = effects;
        this.effectAmmounts = effectAmmounts;
        this.isRightHanded = isRightHanded;
    }
    // public void CreateAbility(){
    //     Ability newAbility = ScriptableObject.CreateInstance<Ability>();
    //     newAbility.Init();
    // }
}
