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
    public Button button;

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
