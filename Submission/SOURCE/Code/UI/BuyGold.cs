using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyGold : MonoBehaviour
{

    public User user;


    public void BuyPouchOfGold()
    {
        if (user.Gem < 10)
            return;
        user.Gold += 1000;
        user.Gem -= 10;
    }
    public void BuyBucketOfGold()
    {
        if (user.Gem < 100)
            return;
        user.Gold += 10000;
        user.Gem -= 100;
    }
    public void BuyWagonOfGold()
    {
        if (user.Gem < 500)
            return;
        user.Gold += 100000;
        user.Gem -= 500;
    }
}
