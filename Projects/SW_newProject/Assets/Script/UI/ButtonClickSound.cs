using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonClickSound : MonoBehaviour
{
    public void PlayButtonClickSound()
    {
        SoundManager.instance.PlaySound("ButtonClick");
    }
}
