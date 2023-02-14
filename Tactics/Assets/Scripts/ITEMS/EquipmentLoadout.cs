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
        [SerializeField] Transform handTransform = null;
        [SerializeField] Weapon weapon = null;
    #endregion
    void Start()
    {
        SpawnWeapon();
    }

    private void SpawnWeapon()
    {
        if(weapon == null) return;
        Animator animator = GetComponentInChildren<Animator>();
        weapon.Spawn(handTransform, animator);
    }
}
}