﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_02 : MonoBehaviour
{
    public GameObject owner;
    public string actionName;

    public void FireAction()
    {
        CombatSysMgr.TriggerAction(actionName, owner);
    }
}
