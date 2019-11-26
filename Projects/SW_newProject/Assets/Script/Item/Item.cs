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

    public int RarePoint = 2;
    public int EpicPoint = 3;
    public int LegendPoint = 4;

    public ItemInfo iteminfo;

    public static Item Copy(Item item)
    {
        Item temp = new Item
        {
            name = item.name,
            equipSlot = item.equipSlot,
            rarity = item.rarity,
            level = item.level,
            icon = item.icon,
            isDefaultItem = item.isDefaultItem,
            MaxHealthModifier = item.MaxHealthModifier,
            ArmorModifier = item.ArmorModifier,
            ManaModifier = item.ManaModifier,
            DamageModifier = item.DamageModifier,
            SpeedModifier = item.SpeedModifier
        };

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

    void Modifier(int val)
    {
        if (MaxHealthModifier != 0)
            MaxHealthModifier += val;
        if (ManaModifier != 0)
            ManaModifier += val;
        if (ArmorModifier != 0)
            ArmorModifier += val;
        if (DamageModifier != 0)
            DamageModifier += val;
        if (SpeedModifier != 0)
            SpeedModifier += val;
    }

    public void Adapting()
    {
        switch (rarity)
        {
            case Rarity.Normal:
                break;
            case Rarity.Rare:
                Modifier(2);
                break;
            case Rarity.Epic:
                Modifier(5);
                break;
            case Rarity.Legendary:
                Modifier(9);
                break;
            default:
                break;
        }

    }

    public void Upgrade()
    {
        switch (rarity)
        {
            case Rarity.Normal:
                break;
            case Rarity.Rare:
                Modifier(RarePoint);
                break;
            case Rarity.Epic:
                Modifier(EpicPoint);
                break;
            case Rarity.Legendary:
                Modifier(LegendPoint);
                break;
            default:
                break;
        }
    }
}


public enum EquipmentSlot { Armor, Weapon, Accessorie }
public enum Rarity { Normal, Rare, Epic, Legendary}