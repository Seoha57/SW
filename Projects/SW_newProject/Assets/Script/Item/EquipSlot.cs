using UnityEngine;
using UnityEngine.UI;


public class EquipSlot : MonoBehaviour
{
    public Image icon;
    public Button removeButton;
    public GameObject removeObject;
    public Item item;
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


    }
    public void ClearSlot()
    {
        item = null;
        icon.sprite = null;
        icon.enabled = false;
        removeButton.interactable = false;
        removeObject.SetActive(false);
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
