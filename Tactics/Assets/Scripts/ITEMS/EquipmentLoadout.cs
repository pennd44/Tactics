using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Items
{
    public class EquipmentLoadout : MonoBehaviour
{
        ///weapon testing
    #region equipment
        [SerializeField] Transform rightHandTransform = null;
        [SerializeField] Transform leftHandTransform = null;
         [SerializeField] Weapon defaultWeapon = null;
        Weapon currentWeapon = null;
    #endregion
    void Start()
    {
        EquipWeapon(defaultWeapon);
    }

   public void EquipWeapon(Weapon weapon)
    {
        currentWeapon = weapon;
        Animator animator = GetComponentInChildren<Animator>();
        Character unit = GetComponent<Character>();
        weapon.Spawn(rightHandTransform, leftHandTransform, unit, animator);
    }
}
}