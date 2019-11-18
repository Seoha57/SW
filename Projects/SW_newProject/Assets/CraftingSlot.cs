using UnityEngine;
using UnityEngine.UI;

public class CraftingSlot : MonoBehaviour
{
    public Image icon;
    public Button removeButton;
    public GameObject removeObject;
    public Item item;
    public FlexibleUIButton flexibleButton;

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
        Inventory.instance.Add(item);
        crafting.instance.UnEquip(item);
        ClearSlot();


    }

    public void Click()
    {
        isSelected = true;
    }
}
