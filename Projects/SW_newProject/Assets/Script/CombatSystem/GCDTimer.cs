using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GCDTimer : MonoBehaviour
{
    Slider m_slider;
    // Start is called before the first frame update
    void Start()
    {
        m_slider = GetComponent<Slider>();
        m_slider.value = 0;
        m_slider.gameObject.SetActive(false);
        ActionEvent Physical = new ActionEvent();
        if (CombatSysMgr.instance.actionEventDic.TryGetValue("GCDInitialize", out Physical))
        {
            Physical.AddListener(GCDbar);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GCDbar(Entity e)
    {
        if (!m_slider.gameObject.activeSelf)
        {
            m_slider.gameObject.SetActive(true);
        }
        else
        {
            if (m_slider.value == m_slider.maxValue)
            {
                m_slider.value = 0;
                return;
            }
            ++m_slider.value;
        }

    }
}
