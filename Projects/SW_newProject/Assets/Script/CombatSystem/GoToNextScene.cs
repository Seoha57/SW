using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoToNextScene : MonoBehaviour
{
    public Button m_button;
    void Start()
    {
        ActionEvent WinEvent = new ActionEvent();
        if (CombatSysMgr.instance.actionEventDic.TryGetValue("AllEnemyIsDead", out WinEvent))
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
