
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public Transform itemsParent;
    public GameObject inventoryUI;

    public Transform equipParent1;
    public Transform equipParent2;
    public GameObject equipUI;


    Inventory inventory;
    EquipmentManager equipmentManager;
    CharacterManager CM;
    Character char1;
    Character char2;

    InventorySlot[] slots;
    EquipSlot[] equipSlots1;
    EquipSlot[] equipSlots2;
    //InventorySlot[] equipSlots;
    private void Start()
    {
        inventory = Inventory.instance;
        //inventory.onItemChangedCallback += UpdateUI;
        CM = CharacterManager.instance;
        equipmentManager = EquipmentManager.instance;
        //char1 = CharacterManager.GetCharacter(0);
        //char2 = CharacterManager.GetCharacter(1);
        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
        equipSlots1 = equipParent1.GetComponentsInChildren<EquipSlot>();
        equipSlots2 = equipParent2.GetComponentsInChildren<EquipSlot>();
        // DontDestroyOnLoad(this);
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

        UpdateInventoryUI();
        UpdateEquipUI();
    }
    public void UpdateInventoryUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if(i<inventory.items.Count)
            {
                slots[i].AddItem(inventory.items[i]);

            }
            else
            {
                slots[i].ClearSlot();
            }
        }
    }

    public void UpdateEquipUI()
    {
    

        for (int i = 0; i < equipSlots1.Length; i++)
        {

            equipSlots1[i].id = 0;
            if (CM.GetCharacter(0).items[i] != null)
                equipSlots1[i].AddItem(CM.GetCharacter(0).items[i]);
            else
                equipSlots1[i].ClearSlot();


            //if (equipmentManager.currentEquipment[i] != null) 
            //    equipSlots1[i].AddItem(equipmentManager.currentEquipment[i]); 
            //else
            //    equipSlots1[i].ClearSlot();

        }

        for (int j = 0; j < equipSlots2.Length; j++)
        {
            equipSlots2[j].id = 1;
            if (CM.GetCharacter(1).items[j] != null)
                equipSlots2[j].AddItem(CM.GetCharacter(1).items[j]);
            else
                equipSlots2[j].ClearSlot();
        }

    }
}
