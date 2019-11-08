using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoToNextScene : MonoBehaviour
{
    public Button m_button;
    void Start()
    {
        ActionEvent LoseEvent = new ActionEvent();
        if (CombatSysMgr.actionEventDic.TryGetValue("PlayerIsDead", out LoseEvent))
        {
            LoseEvent.AddListener(ShowButton);
        }
        ActionEvent WinEvent = new ActionEvent();
        if (CombatSysMgr.actionEventDic.TryGetValue("EnemyIsDead", out WinEvent))
        {
            WinEvent.AddListener(ShowButton);
        }
        m_button.gameObject.SetActive(false);
    }

    void ShowButton(Entity e)
    {
        m_button.gameObject.SetActive(true);
    }
}
