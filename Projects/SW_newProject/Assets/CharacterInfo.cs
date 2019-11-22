using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CharacterInfo : MonoBehaviour
{
  
    public User user;

    public Text Name;
    public Text Exp;
    public Text Exp_required;
    public Text HP;
    public Text Armor;
    public Text Mana;
    public Text Damage;
    public Text Speed;

    public Text level;
    public int ID = 0;

    public Text AttributePoint;

    public GameObject AttributesPointButton;

    CharacterManager CM;


    private void Start()
    {
        CM = CharacterManager.instance;
    }
    private void Update()
    {
        ID = user.SelectedID;
        Name.text = CM.GetCharacter(ID).Name;
        HP.text = CM.GetCharacter(ID).attribute.MaxHealth.ToString();
        Armor.text = CM.GetCharacter(ID).attribute.Armor.ToString();
        Mana.text = CM.GetCharacter(ID).attribute.Mana.ToString();
        Damage.text = CM.GetCharacter(ID).attribute.Damage.ToString();
        Speed.text = CM.GetCharacter(ID).attribute.Speed.ToString();

        level.text = CM.GetCharacter(ID).Level.ToString();
        Exp.text = CM.GetCharacter(ID).XP.ToString();
        Exp_required.text = CM.GetCharacter(ID).XP_required.ToString();
        AttributePoint.text = CM.GetAttributePoint(ID).ToString();

        if (CM.GetAttributePoint(ID) < 1)
            AttributesPointButton.SetActive(false);
        else
            AttributesPointButton.SetActive(true);
    }


}
