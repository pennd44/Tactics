using System.Collections.Generic;
using UnityEngine;
using Game.Items;
public class Pickup : Interactable
{
    [SerializeField] Weapon weapon = null;
    public override void ApplyEffects(Character character){
        character.equipmentLoadout.EquipWeapon(weapon);
        Destroy(gameObject);
    }

    public override bool CanInteract(Character character){
        return true;
    }
}