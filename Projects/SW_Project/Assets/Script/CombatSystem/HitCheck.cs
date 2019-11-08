using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitCheck : MonoBehaviour
{
    public GameObject owner;
    Material m_material;
    private Color temp;
    float timer = 0;
    // Start is called before the first frame update
    void Start()
    {
        m_material = owner.GetComponent<Renderer>().material;
        temp = m_material.color;

        ActionEvent hit = new ActionEvent();
        if (CombatSysMgr.actionEventDic.TryGetValue("TakePhysicalDamage", out hit))
        {
            hit.AddListener(Hitted);
        }
        ActionEvent Combo = new ActionEvent();
        if (CombatSysMgr.actionEventDic.TryGetValue("TakePhysicalComboDamage", out Combo))
        {
            Combo.AddListener(HittedCombo);
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
        if (owner.GetComponent<Entity>().target.GetComponent<Entity>() == e)
        {
            owner.GetComponent<Renderer>().material.color = new Color(255, 0, 0);
        }
    }
    void HittedCombo(Entity e)
    {
        if (owner.GetComponent<Entity>().target.GetComponent<Entity>() == e)
        {
            owner.GetComponent<Renderer>().material.color = new Color(255, 0, 0);
        }
    }
}
