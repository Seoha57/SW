using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitCheck : MonoBehaviour
{
    public GameObject owner;
    Material m_material;
    private Color temp;
    float timer = 0;
    int ownerID;

    // Start is called before the first frame update
    void Start()
    {
        ownerID = owner.GetComponent<Entity>().ID;
        m_material = owner.GetComponent<Renderer>().material;
        temp = m_material.color;

        ActionEvent hit = new ActionEvent();
        if (CombatSysMgr.instance.actionEventDic.TryGetValue("TakePhysicalDamage", out hit))
        {
            hit.AddListener(Hitted);
        }

        ActionEvent combo = new ActionEvent();
        if (CombatSysMgr.instance.actionEventDic.TryGetValue("TakePhysicalComboDamage", out combo))
        {
            combo.AddListener(Hitted);
        }

        ActionEvent heal = new ActionEvent();
        if (CombatSysMgr.instance.actionEventDic.TryGetValue("SelfHeal", out heal))
        {
            heal.AddListener(Recovered);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (owner.GetComponent<Renderer>().material.color != temp)
        {
            timer += Time.deltaTime;
            if (timer > 1)
            {
                owner.GetComponent<Renderer>().material.color = temp;
                timer = 0;
            }
        }
    }

    void Hitted(Entity e)
    {
        if (ownerID == e.target.GetComponent<Entity>().ID)
        {
            owner.GetComponent<Renderer>().material.color = new Color(255, 0, 0);
        }
    }

    void Recovered(Entity e)
    {
        if(ownerID == e.ID)
        {
            owner.GetComponent<Renderer>().material.color = new Color(0, 255, 0);
        }
    }
}
