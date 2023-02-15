using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Ability", menuName = "Abilities/New Ability", order = 0)]
public class Ability : ScriptableObject
{
    [SerializeField] AnimatorOverrideController animatorOverride;
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

    public void OverrideAnimation(Animator animator)
    {
        if(animatorOverride != null){
            animator.runtimeAnimatorController = animatorOverride;
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
