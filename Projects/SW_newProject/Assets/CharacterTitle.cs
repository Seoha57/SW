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
        Name.text = CharacterManager.instance.GetCharacter(ID).Name;
        level.text = CharacterManager.instance.GetCharacter(ID).Level.ToString();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
