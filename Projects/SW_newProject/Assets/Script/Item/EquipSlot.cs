using UnityEngine;
using UnityEngine.UI;


public class EquipSlot : MonoBehaviour
{
    public Image icon;
    public Button removeButton;
    public GameObject removeObject;
    public Item item;

    public Button button;

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
