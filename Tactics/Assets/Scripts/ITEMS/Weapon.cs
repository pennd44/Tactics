using UnityEngine;
namespace Game.Items
{    
    [CreateAssetMenu(fileName = "Weapon", menuName = "Weapons/Make New Weapon", order = 0)]
    public class Weapon : ScriptableObject {
    #region equipment
    [SerializeField] GameObject weaponPrefab = null;
    [SerializeField] AnimatorOverrideController equipmentOverride;
    
    [SerializeField] int weaponRange;
    [SerializeField] float weaponDamage;
    #endregion
   
    public void Spawn(Transform handTransform, Animator animator)
    {
        Instantiate(weaponPrefab, handTransform);
        animator.runtimeAnimatorController = equipmentOverride;
    }
    public float GetDamage(){
        return weaponDamage;
    }
    
    }
}