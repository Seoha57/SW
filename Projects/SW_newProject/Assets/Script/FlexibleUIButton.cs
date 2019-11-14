using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(Button))]
[RequireComponent(typeof(Image))]
public class FlexibleUIButton : FlexibleUI
{
    public enum ButtonType
    {
        Defalut,
        Type1,
        Type2,
        Type3,
        Type4,
        Type5
    }

    Image image;
    Image icon;
    Button button;

    public ButtonType buttontype;

    protected override void OnSkinUI()
    {
        base.OnSkinUI();

        image = GetComponent<Image>();
        icon = transform.Find("Icon").GetComponent<Image>();
        button = GetComponent<Button>();

        button.transition = Selectable.Transition.SpriteSwap;
        button.targetGraphic = image;

        image.sprite = skinData.buttonSprite;
        image.type = Image.Type.Sliced;
        button.spriteState = skinData.buttonSpriteState;

        switch (buttontype)
        {
            case ButtonType.Defalut:
                image.color = skinData.defaultColor;
                icon.sprite = skinData.defaultIcon;
                break;
            case ButtonType.Type1:
                image.color = skinData.type1Color;
                icon.sprite = skinData.type1Icon;
                break;
            case ButtonType.Type2:
                image.color = skinData.type2Color;
                icon.sprite = skinData.type2Icon;

                break;
            case ButtonType.Type3:
                image.color = skinData.type3Color;
                icon.sprite = skinData.type3Icon;
                break;
            case ButtonType.Type4:
                image.color = skinData.type4Color;
                icon.sprite = skinData.type4Icon;
                break;
            case ButtonType.Type5:
                image.color = skinData.type5Color;
                icon.sprite = skinData.type5Icon;
                break;
            default:
                break;
        }

    }
}
