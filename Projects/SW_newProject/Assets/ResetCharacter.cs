using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetCharacter : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        CharacterManager.instance.GetCharacter(0).RESET_ATTRIBUTE();
        CharacterManager.instance.GetCharacter(1).RESET_ATTRIBUTE();
        CharacterManager.instance.GetCharacter(2).RESET_ATTRIBUTE();
    }


}
