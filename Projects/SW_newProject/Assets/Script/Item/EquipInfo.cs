using UnityEngine;
using UnityEngine.UI;

public class EquipInfo : MonoBehaviour
{

    public GameObject window;
    public InventoryUI inventoryUI;


    public Image icon;

    public Text MaxHealthModifier;
    public Text ArmorModifier;
    public Text ManaModifier;
    public Text DamageModifier;
    public Text SpeedModifier;
    public FlexibleUIButton infobox;

    private int ID;
    private void Start()
    {
        window.SetActive(false);
    }

    private void Update()
    {

        ID = inventoryUI.index;
        if(ID!= -1)
        {
            icon.sprite = inventoryUI.slots[ID].item.icon;
            MaxHealthModifier.text = inventoryUI.slots[ID].item.MaxHealthModifier.ToString();
            ArmorModifier.text = inventoryUI.slots[ID].item.ArmorModifier.ToString();
            ManaModifier.text = inventoryUI.slots[ID].item.ManaModifier.ToString();
            DamageModifier.text = inventoryUI.slots[ID].item.DamageModifier.ToString();
            SpeedModifier.text = inventoryUI.slots[ID].item.SpeedModifier.ToString();

            switch (inventoryUI.slots[ID].item.rarity)
            {
                case Rarity.Normal:
                    infobox.buttontype = FlexibleUIButton.ButtonType.Type1;
                    break;
                case Rarity.Rare:
                    infobox.buttontype = FlexibleUIButton.ButtonType.Type2;
                    break;
                case Rarity.Epic:
                    infobox.buttontype = FlexibleUIButton.ButtonType.Type3;
                    break;
                case Rarity.Legendary:
                    infobox.buttontype = FlexibleUIButton.ButtonType.Type4;
                    break;
                case Rarity.Hidden:
                    infobox.buttontype = FlexibleUIButton.ButtonType.Type5;
                    break;
                default:
                    infobox.buttontype = FlexibleUIButton.ButtonType.Type1;
                    break;
            }
          
        }
 
    }
    public void RemoveClick()
    {
        window.SetActive(false);
    }
    public void UseButton()
    {
        inventoryUI.slots[ID].Use();
    }

        
}
