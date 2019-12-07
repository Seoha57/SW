using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RewardManager : MonoBehaviour
{
    private SoundManager SM;
    public GameObject soundMgr;
    public int prevStage;

    public static RewardManager instance;

    public Image icon;


    public List<Item> items = new List<Item>();

    public User user;
    public int Xp = 100;
    public int gold;

    public float normal_prob;
    public float rare_prob;
    public float epic_prob;
    public float legendary_prob;
    public Button button;
    private Item rand_item;
    int ID = 0;

    private void Start()
    {
        soundMgr = GameObject.Find("SoundManager");
        SM = soundMgr.GetComponent<SoundManager>();
        
        prevStage = SM.prevStage;
        ID = user.SelectedID;
        if(instance == null)
        {
            instance = this;
            RandomSpawn();
            gold = Random.Range(20, 100);
        }
        Time.timeScale = 1.0f;
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
        switch (prevStage)
        {
            case 1:
                //legendary_prob = 1.0f;
                break;
            case 2:
                epic_prob = 0.3f;
                legendary_prob = 0.1f;
                break;
            case 3:
                rare_prob = 0.9f;
                epic_prob = 0.6f;
                legendary_prob = 0.3f;
                break;

            default:
                break;
        }

        if (rand < legendary_prob)
        {
            rand_item.rarity = Rarity.Legendary;
            button.GetComponent<Image>().color = ItemColor.LegendColor;



        }
        else if (rand > legendary_prob && rand < epic_prob)
        {
            rand_item.rarity = Rarity.Epic;
            button.GetComponent<Image>().color = ItemColor.EpicColor;

        }
        else if (rand > epic_prob && rand < rare_prob)
        {
            rand_item.rarity = Rarity.Rare;
     
            button.GetComponent<Image>().color = ItemColor.RareColor;

        }
        else
        {
            rand_item.rarity = Rarity.Normal;
            button.GetComponent<Image>().color = ItemColor.NormalColor;
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
