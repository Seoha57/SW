using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    public GameObject owner;
    public string actionName;
    public int skill_no;

    private void Start()
    {
        actionName = owner.GetComponent<Character>().Skills[skill_no];
    }

    public void FireAction()
    {
        CombatSysMgr.instance.TriggerAction(actionName, owner);
    }
}
