﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    Image timerbar;
    float maxTime;
    float timeLeft;
    public int SkillNum;

    // Start is called before the first frame update
    void Start()
    {
        timerbar = GetComponent<Image>();
        maxTime = Actions.GetOGCDDuration(SkillNum);
        timeLeft = maxTime;

        ActionEvent timeReset = new ActionEvent();
        if (SkillNum == 0)
        {
            if (CombatSysMgr.instance.actionEventDic.TryGetValue("OGCD1_Init", out timeReset))
            {
                timeReset.AddListener(TimerReset);
            }
        }
        else if (SkillNum == 1)
        {
            if (CombatSysMgr.instance.actionEventDic.TryGetValue("OGCD2_Init", out timeReset))
            {
                timeReset.AddListener(TimerReset);
            }
        }
        else if (SkillNum == 2)
        {
            if (CombatSysMgr.instance.actionEventDic.TryGetValue("OGCD3_Init", out timeReset))
            {
                timeReset.AddListener(TimerReset);
            }
        }

        ActionEvent endGame = new ActionEvent();
        if (CombatSysMgr.instance.actionEventDic.TryGetValue("PlayerIsDead", out timeReset))
        {
            timeReset.AddListener(TimerStop);
        }
        ActionEvent endGame2 = new ActionEvent();
        if (CombatSysMgr.instance.actionEventDic.TryGetValue("AllEnemyIsDead", out timeReset))
        {
            timeReset.AddListener(TimerStop);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            timerbar.fillAmount = timeLeft / maxTime;
        }
    }

    void TimerReset(Entity e)
    {
        timeLeft = maxTime;
    }

    void TimerStop(Entity e)
    {
        Time.timeScale = 0;
    }
}
