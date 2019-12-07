using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CharacterTitle : MonoBehaviour
{

    public User user;
    public Text Name;
    public Text level;

    public int ID;
    // Start is called before the first frame update
    void Start()
    {
        ID = user.SelectedID;

        if (ID == 0)
            Name.text = "시발";
        else if (ID == 1)
            Name.text = "개좆같네";
        else if (ID == 2)
            Name.text = "머지";

    }

    // Update is called once per frame
    void Update()
    {
       

        //Name.text = CharacterManager.instance.GetCharacter(ID).Name;
        //level.text = CharacterManager.instance.GetCharacter(ID).Level.ToString();
    }
}
