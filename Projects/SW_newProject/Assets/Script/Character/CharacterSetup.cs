using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Character))]
public class CharacterSetup : MonoBehaviour
{

    private void OnEnable()
    {
        Character character = GetComponent<Character>();
        //CharacterManager.RegisterCharacter(transform.name, character);
        Debug.Log("Registed :" + transform.name);
    }

    private void OnDisable()
    {
        Debug.Log("Unregisted :" + transform.name);
       // CharacterManager.UnRegisterCharacter(transform.name);
    }
   
}
