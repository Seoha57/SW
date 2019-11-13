using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment", menuName = "Inventory/Equipment")]
public class Equipment : Item
{


    //public override void Use()
    //{
    //    base.Use();
    //    // Equip the item

    //    EquipmentManager.instance.Equip(this);

    //    CharacterManager.AddMaxHealth(0, MaxHealthModifier);
    //    CharacterManager.AddArmor(0, ArmorModifier);
    //    CharacterManager.AddMana(0, ManaModifier);
    //    CharacterManager.AddDamage(0, DamageModifier);
    //    CharacterManager.AddSpeed(0, SpeedModifier);


    //    // Remove it from the inventory
    //    RemoveFromInventory();
    //}
    //public override void Unequip()
    //{
    //    base.Unequip();
    //    EquipmentManager.instance.Unequip((int)this.equipSlot);

    //    CharacterManager.AddMaxHealth(0, -MaxHealthModifier);
    //    CharacterManager.AddArmor(0, -ArmorModifier);
    //    CharacterManager.AddMana(0, -ManaModifier);
    //    CharacterManager.AddDamage(0, -DamageModifier);
    //    CharacterManager.AddSpeed(0, -SpeedModifier);
    //}
}
