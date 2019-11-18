using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName ="Inventory/Item")]
public class Item : ScriptableObject
{
    new public string name = "New Item";
    public EquipmentSlot equipSlot;
    public Rarity rarity;
    public int level = 1;

    public Sprite icon = null;
    public bool isDefaultItem = false;

    public float MaxHealthModifier;
    public float ArmorModifier;
    public float ManaModifier;
    public float DamageModifier;
    public float SpeedModifier;

    public ItemInfo iteminfo;

    public static Item Copy(Item item)
    {
        Item temp = new Item();

        temp.name = item.name;
        temp.equipSlot = item.equipSlot;
        temp.rarity = item.rarity;
        temp.level = item.level;
        temp.icon = item.icon;
        temp.isDefaultItem = item.isDefaultItem;
        temp.MaxHealthModifier = item.MaxHealthModifier;
        temp.ArmorModifier = item.ArmorModifier;
        temp.ManaModifier = item.ManaModifier;
        temp.DamageModifier = item.DamageModifier;
        temp.SpeedModifier = item.SpeedModifier;

        return temp;
       

        
    }

    public void Equip(int id)
    {
        Debug.Log("Using " + name);
        // Equip the item

        //EquipmentManager.instance.Equip(this);

        CharacterManager.instance.GetCharacter(id).Equip(this);


        // Remove it from the inventory
        RemoveFromInventory();
    }
    public void RemoveFromInventory()
    {
        Inventory.instance.Remove(this);
    }
    public void Unequip(int id)
    {
        Debug.Log("Unequip " + name);

        //EquipmentManager.instance.Unequip((int)this.equipSlot);
        CharacterManager.instance.GetCharacter(id).UnEquip((int)this.equipSlot);
    }

    public void CraftingEquip()
    {
        if(crafting.instance.Equip(this))
            // Remove it from the inventory
            RemoveFromInventory();
    }
    

}


public enum EquipmentSlot { Armor, Weapon, Accessorie }
public enum Rarity { Normal, Rare, Epic, Legendary}