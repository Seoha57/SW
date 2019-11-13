using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemInfo : MonoBehaviour
{
    public Text health;
    public Text armor;
    public Text mana;
    public Text damage;
    public Text speed;

    public Transform itemsParent;
    public Transform equipParent1;
    public Transform equipParent2;

    public CharacterInfo characterinfo;
    public GameObject equipinfo;
    Inventory inventory;
    InventorySlot[] slots;
    EquipSlot[] equipslots1;
    EquipSlot[] equipslots2;



    int SelectedIndex = 100;
    int id = 0;

    bool SelectInventory = false;
    bool SelectEquipment1 = false;
    bool SelectEquipment2 = false;

    public void Info(int index)
    {
        if (slots[index].item == null)
            return;

        health.text = slots[index].item.MaxHealthModifier.ToString();
        armor.text = slots[index].item.ArmorModifier.ToString();
        mana.text = slots[index].item.ManaModifier.ToString();
        damage.text = slots[index].item.DamageModifier.ToString();
        speed.text = slots[index].item.SpeedModifier.ToString();

    }
    public void Info1(int index)
    {
        if (equipslots1[index].item == null)
            return;

        health.text = equipslots1[index].item.MaxHealthModifier.ToString();
        armor.text = equipslots1[index].item.ArmorModifier.ToString();
        mana.text = equipslots1[index].item.ManaModifier.ToString();
        damage.text = equipslots1[index].item.DamageModifier.ToString();
        speed.text = equipslots1[index].item.SpeedModifier.ToString();

    }
    public void Info2(int index)
    {
        if (equipslots2[index].item == null)
            return;

        health.text = equipslots2[index].item.MaxHealthModifier.ToString();
        armor.text = equipslots2[index].item.ArmorModifier.ToString();
        mana.text = equipslots2[index].item.ManaModifier.ToString();
        damage.text = equipslots2[index].item.DamageModifier.ToString();
        speed.text = equipslots2[index].item.SpeedModifier.ToString();

    }
    private void Start()
    {
        inventory = Inventory.instance;
        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
        equipslots1 = equipParent1.GetComponentsInChildren<EquipSlot>();
        equipslots2 = equipParent2.GetComponentsInChildren<EquipSlot>();
    }

    private void Update()
    {
        id = characterinfo.ID;
        for (int i = 0; i < slots.Length; i++)
        {
            if(slots[i].isSelected)
            {
                SelectedIndex = i;
                slots[i].isSelected = false;
                SelectInventory = true;
                SelectEquipment1 = false;
                SelectEquipment2 = false;
            }
        }

        for (int i = 0; i < equipslots1.Length; i++)
        {
            if (equipslots1[i].isSelected)
            {
                SelectedIndex = i;
                equipslots1[i].isSelected = false;
                SelectInventory = false;
                SelectEquipment1 = true;
                SelectEquipment2 = false;

            }
        }

        for (int i = 0; i < equipslots2.Length; i++)
        {
            if (equipslots2[i].isSelected)
            {
                SelectedIndex = i;
                equipslots2[i].isSelected = false;
                SelectInventory = false;
                SelectEquipment1 = false;
                SelectEquipment2 = true;
            }
        }

        if (SelectedIndex != 100 && SelectInventory)
            Info(SelectedIndex);

        if (SelectedIndex != 100 && SelectEquipment1)
            Info1(SelectedIndex);
        if (SelectedIndex != 100 && SelectEquipment2)
            Info2(SelectedIndex);

        if (Input.GetKeyDown(KeyCode.C))
            TEST1();
        if (Input.GetKeyDown(KeyCode.V))
            TEST2();
    }

    public void Equip()
    {
        if (SelectedIndex != 100)
        {

            inventory.items[SelectedIndex].Equip(id);
            SelectedIndex = 100;
        }
            
    }
    public void UnEquip()
    {
        if (SelectedIndex != 100 && SelectEquipment1)
        {
            equipslots1[SelectedIndex].OnRemoveButton();
            SelectedIndex = 100;
        }
        if (SelectedIndex != 100 && SelectEquipment2)
        {
            equipslots2[SelectedIndex].OnRemoveButton();
            SelectedIndex = 100;
        }
    }
    public void SelectCharacter1()
    {
        id = 0;
    }

    public void SelectCharacter2()
    {
        id = 1;


    }

    public void TEST1()
    {
        for (int i = 0; i < 3; ++i)
        {
            if(CharacterManager.instance.GetCharacter(0).items[i]!=null)
                Debug.Log(CharacterManager.instance.GetCharacter(0).items[i].name);
        }
    }
    public void TEST2()
    {
        for (int i = 0; i < 3; ++i)
        {
            if (CharacterManager.instance.GetCharacter(1).items[i] != null)
                Debug.Log(CharacterManager.instance.GetCharacter(1).items[i].name);
        }
    }

}
