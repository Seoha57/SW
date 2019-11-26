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

    private void Start()
    {
        HPBar = GetComponent<Image>();
        maxHP = owner.GetComponent<Entity>().HP;
        HPLeft = maxHP;
    }

    // Update is called once per frame
    void Update()
    {
        if (!owner.activeSelf)
        {
            HPBar.gameObject.SetActive(false);
        }
        HPLeft = owner.GetComponent<Entity>().HP;
        HPBar.fillAmount = HPLeft / maxHP;
    }
}
