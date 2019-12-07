using UnityEngine;
using UnityEngine.UI;

public class CraftingSlot : MonoBehaviour
{
    public Image icon;
    public Button removeButton;
    public GameObject removeObject;
    public Item item;
    public Button button;

    public bool isSelected = false;
    int index;
    public int id = 0;


    public void AddItem(Item newitem)
    {
        item = newitem;
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
        Inventory.instance.Add(item);
        crafting.instance.UnEquip(item);
        ClearSlot();


    }

    public void Click()
    {
        isSelected = true;
    }
}
