using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ItemGenerate : MonoBehaviour
{
    public List<Equipment> items = new List<Equipment>();

    public User user;

    public Image normal_item;
    public Image rare_item;
    public Image epic_item;

    private Item rand_normal_item;
    private Item rand_rare_item;
    private Item rand_epic_item;

    private void Start()
    {
        rand_normal_item = Generate();
        rand_rare_item = Generate();
        rand_epic_item = Generate();

        UpdateImage();
    }
    private void UpdateImage()
    {
        normal_item.sprite = rand_normal_item.icon;
        rare_item.sprite = rand_rare_item.icon;
        epic_item.sprite = rand_epic_item.icon;
    }
    public Item Generate()
    {
        return Item.Copy(items[Random.Range(0, items.Count)]);
    }

    public void BuyNormalItem()
    {
        if (user.Gold < 500)
            return;

        rand_normal_item.rarity = Rarity.Normal;
        rand_normal_item.Adapting();
        Inventory.instance.Add(rand_normal_item);
        rand_normal_item = Generate();
        user.Gold -= 500;
        UpdateImage();

    }
    public void BuyRareItem()
    {
        if (user.Gold < 2000)
            return;
        rand_rare_item.rarity = Rarity.Rare;
        rand_rare_item.Adapting();
        Inventory.instance.Add(rand_rare_item);
        rand_rare_item = Generate();
        user.Gold -= 2000;
        UpdateImage();
    }
    public void BuyEpicItem()
    {
        if (user.Gold < 5000)
            return;
        rand_epic_item.rarity = Rarity.Epic;
        rand_epic_item.Adapting();
        Inventory.instance.Add(rand_epic_item);
        rand_epic_item = Generate();
        user.Gold -= 5000;
        UpdateImage();
    }
}
