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

    public InventoryUI inventoryUI;
    public Button button;
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
                button.GetComponent<Image>().color = ItemColor.NormalColor;
                break;
            case Rarity.Rare:
                button.GetComponent<Image>().color = ItemColor.RareColor;
                break;
            case Rarity.Epic:
                button.GetComponent<Image>().color = ItemColor.EpicColor;
                break;
            case Rarity.Legendary:
                button.GetComponent<Image>().color = ItemColor.LegendColor;
                break;
            case Rarity.Hidden:
                button.GetComponent<Image>().color = ItemColor.HiddenColor;
                break;
            default:
                button.GetComponent<Image>().color = ItemColor.NormalColor;
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

        button.GetComponent<Image>().color = ItemColor.NormalColor;
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
