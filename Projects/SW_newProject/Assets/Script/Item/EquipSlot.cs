using UnityEngine;
using UnityEngine.UI;


public class EquipSlot : MonoBehaviour
{
    public Image icon;
    public Button removeButton;
    public GameObject removeObject;
    public Item item;
    public FlexibleUIButton flexibleButton;

    public bool isSelected = false;
    int index;
    public int id = 0;


    public void AddItem(Item newEquip)
    {
        item = newEquip;
        index = (int)newEquip.equipSlot;
        icon.sprite = item.icon;
        icon.enabled = true;
        removeButton.interactable = true;
        removeObject.SetActive(true);

        switch (item.rarity)
        {
            case Rarity.Normal:
                flexibleButton.buttontype = FlexibleUIButton.ButtonType.Type1;
                break;
            case Rarity.Rare:
                flexibleButton.buttontype = FlexibleUIButton.ButtonType.Type2;
                break;
            case Rarity.Epic:
                flexibleButton.buttontype = FlexibleUIButton.ButtonType.Type3;
                break;
            case Rarity.Legendary:
                flexibleButton.buttontype = FlexibleUIButton.ButtonType.Type4;
                break;
            case Rarity.Hidden:
                flexibleButton.buttontype = FlexibleUIButton.ButtonType.Type5;
                break;
            default:
                flexibleButton.buttontype = FlexibleUIButton.ButtonType.Type1;
                break;
        }

    }
    public void ClearSlot()
    {
        item = null;
        icon.sprite = null;
        icon.enabled = false;
        removeButton.interactable = false;
        removeObject.SetActive(false);
        flexibleButton.buttontype = FlexibleUIButton.ButtonType.Defalut;
    }
    public void OnRemoveButton()
    {

        item.Unequip(id);
        ClearSlot();

    }

    public void UseItem()
    {
 
        if (item != null)
        {
            item.Equip(0);
            //Inventory.instance.Remove(item);               
        }
    }
    public void Click()
    {
        isSelected = true;
    }
}
