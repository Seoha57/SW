using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageSpawn : MonoBehaviour
{
    public GameObject owner;
    public Text m_text;
    float timer = 0;

    void Start()
    {
        m_text.gameObject.SetActive(false);
        ActionEvent Physical = new ActionEvent();
        if (CombatSysMgr.actionEventDic.TryGetValue("TakePhysicalDamage", out Physical))
        {
            Physical.AddListener(GetDamage);
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
        if (owner.GetComponent<Entity>() == e)
        {
            m_text.gameObject.SetActive(true);
            m_text.text = Actions.GetPhysicalDamage().ToString();
        }
    }
}
