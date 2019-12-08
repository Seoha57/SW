using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gold : MonoBehaviour
{
    public User user;
    public Text gold;

    private void Update()
    {
        gold.text = user.Gold.ToString();
    }
}
