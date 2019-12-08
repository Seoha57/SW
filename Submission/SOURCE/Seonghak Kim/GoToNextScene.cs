using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoToNextScene : MonoBehaviour
{
    public Button m_button;
    public Image m_background;
    void Start()
    {
        ActionEvent WinEvent = new ActionEvent();
        if (CombatSysMgr.instance.actionEventDic.TryGetValue("AllEnemyIsDead", out WinEvent))
        {
            WinEvent.AddListener(ShowButton);
        }
        m_button.gameObject.SetActive(false);
        m_background.gameObject.SetActive(false);
    }

    void ShowButton(Entity e)
    {
        m_button.gameObject.SetActive(true);
        m_background.gameObject.SetActive(true);
    }
}
