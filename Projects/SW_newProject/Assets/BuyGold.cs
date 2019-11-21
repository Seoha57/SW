using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyGold : MonoBehaviour
{

    public User user;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BuyPouchOfGold()
    {
        user.Gold += 1000;
        user.Gem -= 10;
    }
    public void BuyBucketOfGold()
    {
        user.Gold += 10000;
        user.Gem -= 100;
    }
    public void BuyWagonOfGold()
    {
        user.Gold += 100000;
        user.Gem -= 500;
    }
}
