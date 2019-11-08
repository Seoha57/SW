using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RewardManager : MonoBehaviour
{
    public static RewardManager instance;

    public Image icon;
    public List<Equipment> items = new List<Equipment>();
    public User user;
    public int Xp = 100;
    public int gold;
    
    private Equipment rand_item;

    private void Awake()
    {
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

    public void RandomSpawn()
    {
       
        rand_item = items[Random.Range(0, items.Count)];
       
        icon.sprite = rand_item.icon;
        Debug.Log("Random Item Spawn " + rand_item.name);
    }

    public void Save()
    {
        Inventory.instance.Add(rand_item);
        CharacterManager.instance.SetExp(0, CharacterManager.instance.GetExp(0)+ Xp);
        user.SetGold(gold);
    }

}
