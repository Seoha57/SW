using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class crafting : MonoBehaviour
{

    #region Singleton
    public static crafting instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;

        }
    }
    #endregion

    public User user;

    public List<Item> items = new List<Item>();

    public Transform itemsParent;

    public GameObject UpgradeButton;
    public FlexibleUIButton Result;
    public Image icon;


    CraftingSlot[] slots;
    Item ResultItem;
    Rarity ResultRarity;

    public int space = 2;
    int ID = 0;
    private void Start()
    {
        if (itemsParent != null)
            slots = itemsParent.GetComponentsInChildren<CraftingSlot>();

        UpgradeButton.SetActive(false);
    }

    private void Update()
    {
        UpdateCrafting();
    }

    public void UpdateCrafting()
    {
        ID = user.SelectedID;
        for (int i = 0; i < slots.Length; i++)
        {

            if (i < items.Count)
            {
                slots[i].AddItem(items[i]);

            }
            else
            {
                slots[i].ClearSlot();
            }

        }
        if(items.Count >= 2)
        {
            if (slots[0].item.rarity == slots[1].item.rarity)
            {

                UpgradeButton.SetActive(true);
                AddResult(slots[0].item);


            }
           
        }
        else
        {
            ClearResult();
            UpgradeButton.SetActive(false);
        }

    }

    public bool Equip(Item newItem)
    {
        if(!newItem.isDefaultItem)
        {

            if(items.Count >= space)
            {
                Debug.Log("Not enough room.");
                return false;
            }
            items.Add(newItem);
          
        }
        return true;
    }
    public void UnEquip(Item item)
    {
        items.Remove(item);
    }

    public void AddResult(Item newItem)
    {
        icon.sprite = newItem.icon;
        icon.enabled = true;

        switch (newItem.rarity)
        {
            case Rarity.Normal:
                Result.buttontype = FlexibleUIButton.ButtonType.Type2;
                ResultRarity = Rarity.Rare;
                break;
            case Rarity.Rare:
                Result.buttontype = FlexibleUIButton.ButtonType.Type3;
                ResultRarity = Rarity.Epic;
                break;
            case Rarity.Epic:
                Result.buttontype = FlexibleUIButton.ButtonType.Type4;
                ResultRarity = Rarity.Legendary;
                break;
            case Rarity.Legendary:
                Result.buttontype = FlexibleUIButton.ButtonType.Type5;
                ResultRarity = Rarity.Hidden;
                break;
            default:
                Result.buttontype = FlexibleUIButton.ButtonType.Type1;
                break;
        }


    }
    public void ClearResult()
    {
        icon.sprite = null;
        icon.enabled = false;
        Result.buttontype = FlexibleUIButton.ButtonType.Defalut;
    }


    public void Upgrade()
    {
        ResultItem = Item.Copy(items[0]);
        ResultItem.rarity = ResultRarity;
        ResultItem.Upgrade();
        Inventory.instance.Add(ResultItem);
        ClearResult();
        items.Clear();

    }
}
