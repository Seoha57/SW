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
        }
    }
}
