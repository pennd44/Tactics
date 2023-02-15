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
    [SerializeField] public int hor;
    [SerializeField] public int vert;
    [SerializeField] public int [] effectAmmount;
    // [SerializeField] List<AbilityEffects> abilityEffects;
    // [SerializeField] TargetFilters filter;
    // [SerializeField] AbilityRanges abilityRange;
    // [SerializeField] AbilityAOEs AOEType;


    [SerializeField] public ActionArea areaOfEffect;
    [SerializeField] ActionRange actionRange;
    [SerializeField] List<ActionEffect> effects = new List<ActionEffect>();
    [SerializeField] ActionTargets targetFilter;

    // [SerializeField] public Material greenBeen;
    public Character unit;

    // private void Awake() {
    //     Debug.Log("Ability Awake");
    //     FindAbilityComponents();
    // }
    // public void FindAbilityComponents(){
    //     Debug.Log("hit FindAbilityComps");
    //     FindRangeComponent();
    //     Debug.Log(actionRange != null);
    //     FindAOEComponent();
    //     Debug.Log(areaOfEffect != null);
    //     FindTargetComponent();
    //     Debug.Log(targetFilter != null);
    //     FindEffects();
    //     Debug.Log(effects.Count > 0);
    // }
    // private void FindTargetComponent(){
    //     switch(filter) 
    //     {
    //     case TargetFilters.DeadTarget:
    //         targetFilter = new DeadTarget();
    //         break;
    //     case TargetFilters.LivingTarget:
    //         targetFilter = new LivingTarget();
    //         break;
    //     default:
    //         break;
    //     }
    // }
    // private void FindRangeComponent(){
    //     switch(abilityRange) 
    //     {
    //     case AbilityRanges.ConeRange:
    //         actionRange = new ConeRange(range, int.MaxValue);
    //         // Debug.Log(actionRange);
    //         break;
    //     case AbilityRanges.InfiniteRange:
    //         actionRange = new InfiniteRange(range, int.MaxValue);
    //         // Debug.Log(actionRange);
    //         break;
    //     // case AbilityRanges.LineRange:
    //     //     actionRange = new LineRange();
    //     //     break;
    //     case AbilityRanges.SelfRange:
    //         actionRange = new SelfRange(range, int.MaxValue);
    //         // Debug.Log(actionRange);
    //         break;
    //     case AbilityRanges.SimpleActionRange:
    //         actionRange = new SimpleActionRange(range, int.MaxValue);
    //         // Debug.Log(actionRange);
    //         break;
    //     default:
    //         break;
    //     }
    // }
    // private void FindAOEComponent(){
    //                 // Debug.Log("hit Ability.FindAOEComponent");

    //     switch(AOEType) 
    //     {
    //     case AbilityAOEs.CircleArea:
    //         areaOfEffect = new CircleArea(hor, vert);
    //         break;
    //     case AbilityAOEs.FullRangeArea:
    //         areaOfEffect = new FullRangeArea();
    //         break;
    //     // case AbilityAOEs.LineArea:
    //     //     areaOfEffect = new LineArea();
    //     //     break;
    //     case AbilityAOEs.SingleUnitArea:
    //         areaOfEffect = new SingleUnitArea();
    //         break;
    //     default:
    //         break;
    //     }
    // }
    // private void FindEffects(){
    //         // Debug.Log("hit Ability.FindEffects");

    //     // for (int i = 0; i < abilityEffects.Count; i++)
    //     // {
            
    //     // }
    //     effects = new List<ActionEffect>();
    //     if (abilityEffects.Contains(AbilityEffects.DamageEffect)){
    //         effects.Add(new DamageEffect());
    //     }
    // }
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
        return actionRange.GetTilesInRange(board);
    }
    public virtual List<Tile> GetTilesInAOE(Board board, Tile tile){
        return areaOfEffect.GetTilesInArea(board, tile);
    }
    public virtual void Use(List<Tile> tiles){
        Debug.Log("Tiles Count in Use " + tiles.Count);
        for (int i = 0; i < tiles.Count; i++)
        {
            Debug.Log(i);
            Debug.Log(tiles[i]);
            if(!targetFilter.CheckHit(tiles[i]))
            {
                continue;
            }
                Debug.Log("Target found!");
            for (int j = 0; j < effects.Count; j++)
            {
                Debug.Log("Inside effects for loop");
                Debug.Log("j = " + j);
                effects[j].AffectTarget(tiles[i].content);
            }
        }
    }

    //Debug Method
    public void LogComponents(){
        Debug.Log(actionRange);
        Debug.Log(areaOfEffect);
        Debug.Log(targetFilter);
        Debug.Log(effects);
    }
}
