﻿using System.Collections;
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
    }
}
