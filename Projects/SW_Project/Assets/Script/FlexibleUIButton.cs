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
        Play,
        Legends,
        Item,
        Store
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
            case ButtonType.Play:
                image.color = skinData.playColor;
                icon.sprite = skinData.playIcon;
                break;
            case ButtonType.Legends:
                image.color = skinData.legendsColor;
                icon.sprite = skinData.legendsIcon;

                break;
            case ButtonType.Item:
                image.color = skinData.itemColor;
                icon.sprite = skinData.itemIcon;
                break;
            case ButtonType.Store:
                image.color = skinData.storeColor;
                icon.sprite = skinData.storeIcon;
                break;
            default:
                break;
        }

    }
}
