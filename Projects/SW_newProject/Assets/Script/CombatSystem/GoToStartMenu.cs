using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoToStartMenu : MonoBehaviour
{
    public Button m_button;
    // Start is called before the first frame update
    void Start()
    {
        ActionEvent LoseEvent = new ActionEvent();
        if (CombatSysMgr.instance.actionEventDic.TryGetValue("PlayerIsDead", out LoseEvent))
        {
            LoseEvent.AddListener(ShowButton);
        }
        m_button.gameObject.SetActive(false);
    }

    void ShowButton(Entity e)
    {
        m_button.gameObject.SetActive(true);
    }
}
