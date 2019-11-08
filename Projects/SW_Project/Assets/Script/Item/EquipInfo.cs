using UnityEngine;
using UnityEngine.UI;

public class EquipInfo : MonoBehaviour
{

    [Header("Equip Info")]
    public EquipSlot equipSlot;
    public Text MaxHealthModifier;
    public Text ArmorModifier;
    public Text ManaModifier;
    public Text DamageModifier;
    public Text SpeedModifier;

    private void Start()
    {
        //MaxHealthModifier.text = equipSlot.equip.MaxHealthModifier.ToString();
        //ArmorModifier.text = equipSlot.equip.ArmorModifier.ToString();
        //ManaModifier.text = equipSlot.equip.ManaModifier.ToString();
        //DamageModifier.text = equipSlot.equip.DamageModifier.ToString();
        //SpeedModifier.text = equipSlot.equip.SpeedModifier.ToString();


    }

    private void Update()
    {
       
    }
}
