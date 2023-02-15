using UnityEngine;
using System.Collections.Generic;

namespace Game.Items
{    
    [CreateAssetMenu(fileName = "Weapon", menuName = "Weapons/Make New Weapon", order = 0)]
    public class Weapon : ScriptableObject {
    #region equipment
    [SerializeField] GameObject weaponPrefab = null;
    [SerializeField] AnimatorOverrideController equipmentOverride;
    
    [SerializeField] int weaponRange;
    [SerializeField] float weaponDamage;
    [SerializeField] List<Ability> skillsGranted;
    #endregion
   
    public void Spawn(Transform handTransform, Animator animator)
    {
        Instantiate(weaponPrefab, handTransform);
        animator.runtimeAnimatorController = equipmentOverride;
    }
    public void GiveSkills(Character unit){
        for(int i = 0; i < skillsGranted.Count; i++){
            unit.AquireSkill(skillsGranted[i]);
        }
    }
    public float GetDamage(){
        return weaponDamage;
    }

    }
}