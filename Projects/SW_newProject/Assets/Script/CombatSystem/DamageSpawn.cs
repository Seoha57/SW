using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageSpawn : MonoBehaviour
{
    public GameObject owner;
    public Text m_text;
    float timer = 0;
    Vector3 originalPos;

    void Start()
    {
        originalPos = m_text.gameObject.transform.position;
        m_text.gameObject.SetActive(false);
        ActionEvent Physical = new ActionEvent();
        if (CombatSysMgr.instance.actionEventDic.TryGetValue("TakeDamage", out Physical))
        {
            Physical.AddListener(GetDamage);
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
            m_text.gameObject.transform.position += new Vector3(0, Time.deltaTime * 5f, 0);
            if (timer > 1)
            {
                m_text.gameObject.transform.position = originalPos;
                m_text.gameObject.SetActive(false);
                timer = 0;
            }
        }
    }

    void GetDamage(Entity e)
    {
        if (owner.GetComponent<Entity>().ID == e.target.GetComponent<Entity>().ID)
        {
            m_text.gameObject.SetActive(true);
            m_text.text = Actions.GetPhysicalDamage().ToString();
            m_text.fontStyle = FontStyle.Normal;
            m_text.color = new Color(245, 165, 67);
            timer = 0;
        }
    }

    void GetRecovery(Entity e)
    {
        if(owner.GetComponent<Entity>().ID == e.ID)
        {
            m_text.gameObject.SetActive(true);
            m_text.text = Actions.GetRecoveryValue().ToString();
            m_text.fontStyle = FontStyle.Normal;
            m_text.color = new Color(0, 255, 0);
            timer = 0;
        }
    }
}
