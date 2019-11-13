using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Button/Flexible UI Data")]
public class FlexibleUIData : ScriptableObject
{
    public Sprite buttonSprite;
    public SpriteState buttonSpriteState;


    public Color defaultColor;
    public Sprite defaultIcon;

    public Color type1Color;
    public Sprite type1Icon;

    public Color type2Color;
    public Sprite type2Icon;

    public Color type3Color;
    public Sprite type3Icon;

    public Color type4Color;
    public Sprite type4Icon;

}
