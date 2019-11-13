using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_01 : MonoBehaviour
{
    public GameObject owner;
    public string actionName;

    public void FireAction()
    {
        CombatSysMgr.instance.TriggerAction(actionName, owner);
    }
}
