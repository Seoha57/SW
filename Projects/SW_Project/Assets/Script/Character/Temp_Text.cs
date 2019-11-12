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

    }

 
}
