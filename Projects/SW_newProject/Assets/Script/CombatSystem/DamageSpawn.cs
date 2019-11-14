using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageSpawn : MonoBehaviour
{
    public GameObject owner;
    public Text m_text;
    float timer = 0;
    int ownerID;

    void Start()
    {
        ownerID = owner.GetComponent<Entity>().ID;
        m_text.gameObject.SetActive(false);
        ActionEvent Physical = new ActionEvent();
        if (CombatSysMgr.instance.actionEventDic.TryGetValue("TakePhysicalDamage", out Physical))
        {
            Physical.AddListener(GetDamage);
        }

        ActionEvent combo = new ActionEvent();
        if (CombatSysMgr.instance.actionEventDic.TryGetValue("TakePhysicalComboDamage", out combo))
        {
            combo.AddListener(GetComboDamage);
        }

        ActionEvent heal = new ActionEvent();
        if (CombatSysMgr.instance.actionEventDic.TryGetValue("SelfHeal", out heal))
        {
            heal.AddListener(GetRecovery);
        }
    }

    private void Update()
    {
        if (m_text.gameObject.activeSelf)
        {
            timer += Time.deltaTime;
            if (timer > 1)
            {
                m_text.gameObject.SetActive(false);
                timer = 0;
            }
        }
    }

    void GetDamage(Entity e)
    {
        if (ownerID == e.target.GetComponent<Entity>().ID)
        {
            m_text.gameObject.SetActive(true);
            m_text.text = Actions.GetPhysicalDamage().ToString();
            m_text.fontStyle = FontStyle.Normal;
            m_text.color = new Color(0, 0, 0);
        }
    }

    void GetComboDamage(Entity e)
    {
        if (ownerID == e.target.GetComponent<Entity>().ID)
        {
            m_text.gameObject.SetActive(true);
            m_text.text = Actions.GetPhysicalDamage().ToString();
            m_text.fontStyle = FontStyle.Bold;
            m_text.color = new Color(234, 255, 0);
        }
    }

    void GetRecovery(Entity e)
    {
        if(ownerID == e.ID)
        {
            m_text.gameObject.SetActive(true);
            m_text.text = Actions.GetPhysicalDamage().ToString();
            m_text.fontStyle = FontStyle.Normal;
            m_text.color = new Color(0, 255, 0);
        }
    }
}
