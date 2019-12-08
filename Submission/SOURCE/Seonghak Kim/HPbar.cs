using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPbar : MonoBehaviour
{
    public GameObject owner;
    Image HPBar;
    float maxHP;
    float HPLeft;

    bool isFirst = true;

    private void Start()
    {
        isFirst = true;
        HPBar = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isFirst)
        {
            maxHP = owner.GetComponent<Entity>().HP;
            HPLeft = maxHP;
            isFirst = false;
        }

        if (!owner.activeSelf)
        {
            HPBar.gameObject.SetActive(false);
        }
        HPLeft = owner.GetComponent<Entity>().HP;
        HPBar.fillAmount = HPLeft / maxHP;
    }
}
