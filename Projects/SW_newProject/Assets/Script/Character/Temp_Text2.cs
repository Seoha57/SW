using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Temp_Text2 : MonoBehaviour
{
    //Reward
    public Text type;

    //XP
    public Text xp;
    public Text gold;

    //public Item item;
    public User user;
    private int reward_xp;
    private int reward_gold;

    // Start is called before the first frame update
    void Start()
    {
        reward_xp = RewardManager.instance.GetXP();
        reward_gold = RewardManager.instance.GetRandomGold();
        xp.text = "+" + reward_xp;
        gold.text ="+"+ reward_gold;

    }

    // Update is called once per frame
    void Update()
    {

        type.text = RewardManager.instance.GetRandomItem().name;
    }

    public void Save()
    {
      
        RewardManager.instance.Save();

        //ItemManager.instance.AddItem(item);
        //CharacterManager.SetExp(0, CharacterManager.GetExp(0)+reward_xp);
        //user.SetGold(reward_gold);

        //Inventory.instance.Add(item.itemtest);
    }
}
