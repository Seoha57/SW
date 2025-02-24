﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinAndLose : MonoBehaviour
{
    public Text m_text;

    void Start()
    {
        ActionEvent LoseEvent = new ActionEvent();
        if (CombatSysMgr.instance.actionEventDic.TryGetValue("PlayerIsDead", out LoseEvent))
        {
            LoseEvent.AddListener(Lose);
        }
        ActionEvent WinEvent = new ActionEvent();
        if (CombatSysMgr.instance.actionEventDic.TryGetValue("AllEnemyIsDead", out WinEvent))
        {
            WinEvent.AddListener(Win);
        }
        m_text.gameObject.SetActive(false);
    }

    void Win(Entity e)
    {
        m_text.gameObject.SetActive(true);
        m_text.text = "Clear this Dungeon";
    }

    void Lose(Entity e)
    {
        m_text.gameObject.SetActive(true);
        m_text.text = "Fail to clear this Stage";
    }
}
