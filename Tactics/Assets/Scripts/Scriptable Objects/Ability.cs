using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability : ScriptableObject
{
    public new string name;
    [SerializeField] public int healthCost = 0;
    [SerializeField] public int staminaCost;
    [SerializeField] public int kiCost;
    [SerializeField] public int range;
    [SerializeField] List<AbilityEffects> abilityEffects;
    [SerializeField] TargetFilters filter;
    [SerializeField] AbilityRanges abilityRange;
    [SerializeField] AbilityAOEs AOEType;


    [SerializeField] public ActionArea areaOfEffect;
    [SerializeField] ActionRange actionRange;
    [SerializeField] List<ActionEffect> effects = new List<ActionEffect>();
    [SerializeField] ActionTargets targetFilter;
    public Character unit;

    public void FindAbilityComponents(){
                    Debug.Log("hit Ability.FindAbilityComponents");

        FindRangeComponent();
        FindAOEComponent();
        FindTargetComponent();
        FindEffects();
    }
    private void FindTargetComponent(){
        switch(filter) 
        {
        case TargetFilters.DeadTarget:
            targetFilter = new DeadTarget();
            break;
        case TargetFilters.LivingTarget:
            targetFilter = new LivingTarget();
            break;
        default:
            break;
        }
    }
    private void FindRangeComponent(){
        switch(abilityRange) 
        {
        case AbilityRanges.ConeRange:
            actionRange = new ConeRange();
            break;
        case AbilityRanges.InfiniteRange:
            actionRange = new InfiniteRange();
            break;
        // case AbilityRanges.LineRange:
        //     actionRange = new LineRange();
        //     break;
        case AbilityRanges.SelfRange:
            actionRange = new SelfRange();
            break;
        case AbilityRanges.SimpleActionRange:
            actionRange = new SimpleActionRange();
            break;
        default:
            break;
        }
    }
    private void FindAOEComponent(){
                    Debug.Log("hit Ability.FindAOEComponent");

        switch(AOEType) 
        {
        case AbilityAOEs.CircleArea:
            areaOfEffect = new CircleArea();
                    Debug.Log(areaOfEffect);
            break;
        case AbilityAOEs.FullRangeArea:
            areaOfEffect = new FullRangeArea();
                    Debug.Log(areaOfEffect);
            break;
        // case AbilityAOEs.LineArea:
        //     areaOfEffect = new LineArea();
        //     break;
        case AbilityAOEs.SingleUnitArea:
            areaOfEffect = new SingleUnitArea();
                    Debug.Log(areaOfEffect);
            break;
        default:
            break;
        }
                    Debug.Log(areaOfEffect);
    }
    private void FindEffects(){
            Debug.Log("hit Ability.FindEffects");

        // for (int i = 0; i < abilityEffects.Count; i++)
        // {
            
        // }
        if (abilityEffects.Contains(AbilityEffects.DamageEffect)){
            effects.Add(new DamageEffect());
        }
    }
    // ActionCost?
    public void OnSelectAbility(){
        Debug.Log("hit Ability.OnSelectAbility");
        FindAbilityComponents();
        actionRange.unit = unit;

        for (int i = 0; i < effects.Count; i++)
        {
            effects[i].unit = unit;
        }
    }
    public virtual List<Tile> GetSelectableTiles(Board board){
        // actionRange.unit = unit;
        return actionRange.GetTilesInRange(board);
    }
    public virtual List<Tile> GetTilesInAOE(Board board, Tile tile){
        return areaOfEffect.GetTilesInArea(board, tile);
    }
    public virtual void Use(List<Tile> tiles){
        for (int i = 0; i < tiles.Count; i++)
        {
            if(targetFilter.CheckHit(tiles[i]))
            {
                for (int j = 0; j < effects.Count; j++)
                {
                    effects[i].AffectTarget(tiles[i].content);
                }
            }
        }
    }

    //show tiles in range
    //highlight area of effect
    //Use
    //Apply effect to targets in AOE
    //check if tile meets filter requirements
}
