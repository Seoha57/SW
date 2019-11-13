using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    #region Singleton
    public static EquipmentManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
    }
    #endregion

    public Item[] currentEquipment;

    public delegate void OnEquipmentChanged(Item newItem, Item oldItem);
    public OnEquipmentChanged onEquipmentChanged;

    Inventory inventory;
   
    private void Start()
    {
        inventory = Inventory.instance;
        
        int numSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length;
        currentEquipment = new Item[numSlots];
      
    }

    public void Equip(Item newItem)
    {
        int slotIndex = (int)newItem.equipSlot;

        Item oldItem = null;

        if(currentEquipment[slotIndex] != null)
        {
            Debug.Log("Changed! ");
            oldItem = currentEquipment[slotIndex];
            inventory.Add(oldItem);
        }
        if(onEquipmentChanged!=null)
        {
            onEquipmentChanged.Invoke(newItem, oldItem);
        }
        currentEquipment[slotIndex] = newItem;
    }

    public void Unequip (int slotIndex)
    {
        if(currentEquipment[slotIndex ]!= null)
        {
            Item oldItem = currentEquipment[slotIndex];
            inventory.Add(oldItem);

            currentEquipment[slotIndex] = null;

            if (onEquipmentChanged != null)
            {
                onEquipmentChanged.Invoke(null, oldItem);
            }

        }
    }

    public void UnequipAll()
    {
        Debug.Log("UnequipAll! ");
        for (int i = 0; i < currentEquipment.Length; i++)
        {
            Unequip(i);
        }
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
            UnequipAll();
    }

}
