using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Skill : MonoBehaviour
{
    public GameObject owner;
    public string actionName;
    public Text m_text;
    public int skill_no;
    private bool isFirst = true;

    private void Update()
    {
        if(isFirst)
        {
            Init();
            isFirst = false;
        }
    }
    public void Init()
    {
        actionName = owner.GetComponent<Character>().Skills[skill_no];
        m_text.text = actionName;
        m_text.color = new Color(255, 255, 255);
    }

    public void FireAction()
    {
        CombatSysMgr.instance.TriggerAction(actionName, owner);
    }
}
