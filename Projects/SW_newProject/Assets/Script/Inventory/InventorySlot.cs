using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Image icon;
    public Button removeButton;

    public bool isSelected = false;
    public GameObject infomation;
    public GameObject useObject;
    public GameObject infoObject;
    public Item item;
    public CharacterInfo characterinfo;
    public GameObject removeObject;

    public Image ButtonColor;
    public FlexibleUIButton flexibleButton;

    public InventoryUI inventoryUI;

    bool onclick = false;
    int ID =0;

    public void InfoUse(bool _bool)
    {
        infoObject.SetActive(_bool);
        useObject.SetActive(_bool);
    }

    public void AddItem(Item newItem)
    {
        item = newItem;

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
        InfoUse(false);
    }
    public void OnRemoveButton()
    {
        Inventory.instance.Remove(item);
        infomation.SetActive(false);
        InfoUse(false);
    }

    public void Click()
    {
        Debug.Log("Click " + name);
        if(item != null)
        {
            //InfoUse(!isSelected);
            isSelected = (isSelected) ? false : true;

            InfoUse(!onclick);
        }
            
    }
    public void Info()
    {
        if (item != null)
        {
            infomation.SetActive(true);
            InfoUse(false);
        }
    }
    public void Use()
    {
        ID = characterinfo.ID;
        if (item != null)
        {
            item.Equip(ID);
            InfoUse(false);
            infomation.SetActive(false);

            inventoryUI.index = -1;
        }
      
    }

    public void Crafting()
    {
        if(item != null)
        {
            item.CraftingEquip();
            //InfoUse(false);
            infomation.SetActive(false);
            inventoryUI.index = -1;
        }
    }
  
}
