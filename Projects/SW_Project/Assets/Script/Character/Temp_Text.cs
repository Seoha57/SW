using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Temp_Text : MonoBehaviour
{
    public User user;
    //public Text User_Money;

    public Text Maxhealth;
    public Text Armor;
    public Text Mana;
    public Text Damage;
    public Text Speed;

    //Character Info
    public Text level;
    public Text xp;
    public Text xp_req;

    //Attribute Point
    public Text Attribute_Point;
    public Text CON;
    public Text WIS;
    public Text STR;
    public Text DEX;
    public Text INT;

    public Text info_Health;
    public Text info_Armor;
    public Text info_Mana;
    public Text info_Damage;
    public Text info_Speed;

    CharacterManager CM;

    // Start is called before the first frame update
    void Start()
    {
        CM = CharacterManager.instance;
    }

    // Update is called once per frame
    void Update()
    {
        //User_Money.text = user.Gold.ToString();
        Maxhealth.text = CM.GetCharacter(0).attribute.MaxHealth.ToString();
        Armor.text = CM.GetCharacter(0).attribute.Armor.ToString();
        Mana.text = CM.GetCharacter(0).attribute.Mana.ToString();
        Damage.text = CM.GetCharacter(0).attribute.Damage.ToString();
        Speed.text = CM.GetCharacter(0).attribute.Speed.ToString();

        level.text = CM.GetCharacter(0).Level.ToString();
        xp.text = CM.GetCharacter(0).XP.ToString();
        xp_req.text = CM.GetCharacter(0).XP_required.ToString();


        Attribute_Point.text = CM.GetCharacter(0).attributePoint.ToString();

        CON.text = CM.GetCharacter(0).AD.CON.ToString();
        WIS.text = CM.GetCharacter(0).AD.WIS.ToString();
        STR.text = CM.GetCharacter(0).AD.STR.ToString();
        DEX.text = CM.GetCharacter(0).AD.DEX.ToString();
        INT.text = CM.GetCharacter(0).AD.INT.ToString();


       
        //info_Health.text = Inventory.instance.items[0].MaxHealthModifier.ToString();
        //info_Health.text = Inventory.instance.items[0].MaxHealthModifier.ToString();
        //info_Health.text = Inventory.instance.items[0].MaxHealthModifier.ToString();
        //info_Health.text = Inventory.instance.items[0].MaxHealthModifier.ToString();
        //info_Health.text = Inventory.instance.items[0].MaxHealthModifier.ToString();
    }

 
}
