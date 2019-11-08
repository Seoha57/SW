using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Image icon;
    public Button removeButton;
    public bool isSelected = false;

    public Item item;


    public void AddItem(Item newItem)
    {
        item = newItem;

        icon.sprite = item.icon;

        icon.enabled = true;
        removeButton.interactable = true;


    }
    public void ClearSlot()
    {
        item = null;
        icon.sprite = null;
        icon.enabled = false;
        removeButton.interactable = false;
    }
    public void OnRemoveButton()
    {
        Inventory.instance.Remove(item);
    }

    public void Click()
    {
        Debug.Log("Click " + name);
        if(item != null)
            isSelected = (isSelected)? false:true;
    }

    public void UseItem()
    {
        if (item != null)
        {
            item.Equip(0);
            //Inventory.instance.Remove(item);               
        }
    }

}
