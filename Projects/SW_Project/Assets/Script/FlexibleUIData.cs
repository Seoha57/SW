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

    public Color playColor;
    public Sprite playIcon;

    public Color legendsColor;
    public Sprite legendsIcon;

    public Color itemColor;
    public Sprite itemIcon;

    public Color storeColor;
    public Sprite storeIcon;

}
