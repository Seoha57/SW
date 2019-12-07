using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TextManager : MonoBehaviour
{
    public User user;
    public Text characterName;
    public Text characterLevel;

    int ID;
    void Start()
    {
        ID = user.SelectedID;
    }

    // Update is called once per frame
    void Update()
    {

        characterName.text = CharacterManager.instance.GetCharacter(ID).Name;
        characterLevel.text = CharacterManager.instance.GetCharacter(ID).Level.ToString();

    }
  
}
