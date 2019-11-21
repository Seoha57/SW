using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RewardManager : MonoBehaviour
{
    public static RewardManager instance;

    public Image icon;
    public FlexibleUIButton flexibleButton;
    public List<Equipment> items = new List<Equipment>();

    public User user;
    public int Xp = 100;
    public int gold;

    public float normal_prob;
    public float rare_prob;
    public float epic_prob;
    public float legendary_prob;

    
    private Item rand_item;
    int ID = 0;

    private void Awake()
    {
        ID = user.SelectedID;
        if(instance == null)
        {
            instance = this;
            RandomSpawn();
            gold = Random.Range(20, 100);
        }

    }
    

    public Item GetRandomItem()
    {
        return rand_item;
    }
    public int GetXP()
    {
        return Xp;
    }
    public int GetRandomGold()
    {
        return gold;
    }
    public void Prob()
    {
        float rand = Random.Range(1, 100000f) / 100000f;
        if (rand < legendary_prob)
        {
            rand_item.rarity = Rarity.Legendary;
            flexibleButton.buttontype = FlexibleUIButton.ButtonType.Type4;



        }
        else if (rand > legendary_prob && rand < epic_prob)
        {
            rand_item.rarity = Rarity.Epic;
            flexibleButton.buttontype = FlexibleUIButton.ButtonType.Type3;

        }
        else if (rand > epic_prob && rand < rare_prob)
        {
            rand_item.rarity = Rarity.Rare;
            flexibleButton.buttontype = FlexibleUIButton.ButtonType.Type2;

        }
        else
        {
            rand_item.rarity = Rarity.Normal;
            flexibleButton.buttontype = FlexibleUIButton.ButtonType.Type1;
        }

        rand_item.Adapting();
        Debug.Log("Rarity : "+rand_item.rarity);
    }
    public void RandomSpawn()
    {

        gold = Random.Range(20, 100);
        rand_item = Item.Copy(items[Random.Range(0, items.Count)]);
        Prob();
        


        icon.sprite = rand_item.icon;
        Debug.Log("Random Item Spawn " + rand_item.name);
    }

    public void Save()
    {
        Inventory.instance.Add(rand_item);
        CharacterManager.instance.SetExp(ID, CharacterManager.instance.GetExp(ID) + Xp);
        user.SetGold(gold);
    }

}
