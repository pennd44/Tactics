using UnityEngine;
using System.Collections.Generic;

namespace Game.Items
{    
    [CreateAssetMenu(fileName = "Weapon", menuName = "Weapons/Make New Weapon", order = 0)]
    public class Weapon : ScriptableObject {
    #region equipment
    [SerializeField] GameObject equippedPrefab = null;
    [SerializeField] AnimatorOverrideController equipmentOverride;
    
    [SerializeField] int weaponRange;
    [SerializeField] float weaponDamage;
    [SerializeField] List<Ability> skillsGranted;
    [SerializeField] bool isRightHanded = true;
    #endregion
   
    public void Spawn(Transform rightHand, Transform leftHand, Character unit, Animator animator)
    {   
        if(equippedPrefab != null){
        Transform handTransform;
            if (isRightHanded) handTransform = rightHand;
            else handTransform = leftHand;
            Instantiate(equippedPrefab, handTransform);
        }
        // animator.runtimeAnimatorController = equipmentOverride;
        GiveSkills(unit);

    }
    private void GiveSkills(Character unit){
        for(int i = 0; i < skillsGranted.Count; i++){
            unit.AquireSkill(skillsGranted[i]);
        }
    }
    public float GetDamage(){
        return weaponDamage;
    }

    }
}