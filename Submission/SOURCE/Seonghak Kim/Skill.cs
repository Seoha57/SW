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
    private Color org_color;

    void Start()
    {
        ActionEvent ComboStart = new ActionEvent();
        if (CombatSysMgr.instance.actionEventDic.TryGetValue("ComboAttackStart", out ComboStart))
        {
            ComboStart.AddListener(ComboStarted);
        }
        ActionEvent ComboFinish = new ActionEvent();
        if (CombatSysMgr.instance.actionEventDic.TryGetValue("ComboAttackFinish", out ComboFinish))
        {
            ComboFinish.AddListener(ComboFinished);
        }
        org_color = this.GetComponent<Image>().color;
    }

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
        m_text.fontSize = 12;
    }

    public void FireAction()
    {
        CombatSysMgr.instance.TriggerAction(actionName, owner);
    }

    private void ComboStarted(Entity e)
    {
        if (owner.GetComponent<Entity>().ID == e.ID && actionName != "Combo Attack")
        {
            this.GetComponent<Image>().color = new Color(0, 0, 0, 0.5f);
        }
    }
    private void ComboFinished(Entity e)
    {
        if (owner.GetComponent<Entity>().ID == e.ID)
        {
            this.GetComponent<Image>().color = org_color;
        }
    }
}
