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
    public Image infoIcon;

    int ID =0;

    void InfoUse(bool _bool)
    {
        infoObject.SetActive(_bool);
        useObject.SetActive(_bool);
    }

    public void AddItem(Item newItem)
    {
        item = newItem;

        icon.sprite = item.icon;
        icon.enabled = true;


        infoIcon.sprite = item.icon;
        infoIcon.enabled = true;

        removeButton.interactable = true;
        removeObject.SetActive(true);


    }
    public void ClearSlot()
    {
        item = null;
        icon.sprite = null;
        icon.enabled = false;

        infoIcon.sprite = null;
        infoIcon.enabled = false;

        removeButton.interactable = false;
        removeObject.SetActive(false);
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
            InfoUse(!isSelected);
            isSelected = (isSelected) ? false : true;

           
        }
            
    }

    public void UseItem()
    {
        if (item != null)
        {
            item.Equip(0);
            //Inventory.instance.Remove(item);               
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
        }
      
    }
    public void RemoveInfo()
    {
        infomation.SetActive(false);
    }
  
}
