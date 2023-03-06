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

    const string weaponName = "Weapon";
   
    public void Spawn(Transform rightHand, Transform leftHand, Character unit, Animator animator)
    {   
        DestroyOldWeapon(rightHand, leftHand);
        if(equippedPrefab != null)
            {
                Transform handTransform = GetTransform(rightHand, leftHand);
                GameObject weapon = Instantiate(equippedPrefab, handTransform);
                weapon.name = weapon.name;
            }
            // animator.runtimeAnimatorController = equipmentOverride;
            GiveSkills(unit);

    }
    private void DestroyOldWeapon(Transform rightHand, Transform leftHand)
    {
        Transform oldWeapon = rightHand.Find(weaponName);
        if(oldWeapon == null)
        {
            oldWeapon = leftHand.Find(weaponName);
        }
        if(oldWeapon == null){
            return;
        }
        oldWeapon.name = "DESTROYING";
        Destroy(oldWeapon.gameObject);
    }

        private Transform GetTransform(Transform rightHand, Transform leftHand)
        {
            Transform handTransform;
            if (isRightHanded) handTransform = rightHand;
            else handTransform = leftHand;
            return handTransform;
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