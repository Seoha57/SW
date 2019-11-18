
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public User user;
    public Transform itemsParent;
    public GameObject inventoryUI;

    public Transform equipParent;
    public GameObject equipUI;

    Inventory inventory;
    EquipmentManager equipmentManager;
    CharacterManager CM;

    InventorySlot[] slots;
    EquipSlot[] equipSlots;

    int ID;

    public int index = -1;

    private void Start()
    {
        inventory = Inventory.instance;
        CM = CharacterManager.instance;
        equipmentManager = EquipmentManager.instance;
        if(itemsParent !=null)
            slots = itemsParent.GetComponentsInChildren<InventorySlot>();
        if(equipParent!=null)
            equipSlots = equipParent.GetComponentsInChildren<EquipSlot>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.M))
        {
            inventoryUI.SetActive(!inventoryUI.activeSelf);
        }
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            equipUI.SetActive(!equipUI.activeSelf);
        }
        if (itemsParent != null)
            UpdateInventoryUI();
        if (equipParent != null)
            UpdateEquipUI();
    }
    public void UpdateInventoryUI()
    {


        for (int i = 0; i < slots.Length; i++)
        {
            if(i<inventory.items.Count)
            {
                slots[i].AddItem(inventory.items[i]);

                if (slots[i].isSelected)
                {
                    if(i == index)
                    {
                        index = -1;
                        continue;
                    }
                    index = i;
                    slots[i].isSelected = false;
                }

                if (i == index)
                    slots[i].InfoUse(true);
                else
                    slots[i].InfoUse(false);
            }
            else
            {
                slots[i].ClearSlot();
            }
        }

    }

    public void UpdateEquipUI()
    {

        ID = user.SelectedID;
        for (int i = 0; i < equipSlots.Length; i++)
        {

            equipSlots[i].id = ID;
            if (CM.GetCharacter(ID).items[i] != null)
                equipSlots[i].AddItem(CM.GetCharacter(ID).items[i]);
            else
                equipSlots[i].ClearSlot();

        }

    }
}
