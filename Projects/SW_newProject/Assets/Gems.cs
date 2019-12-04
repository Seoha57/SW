using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Gems : MonoBehaviour
{
    public User user;
    public Text gems;

    private void Update()
    {
        gems.text = user.Gem.ToString();
        if (Input.GetKeyDown(KeyCode.PageDown))
        {
            user.Gem = 0;
            user.Gold= 0;
        }
        if (Input.GetKeyDown(KeyCode.PageUp))
        {
            user.Gem += 1000000;
        }
    }
}
