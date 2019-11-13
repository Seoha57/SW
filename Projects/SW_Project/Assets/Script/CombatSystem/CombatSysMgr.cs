using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class ActionEvent : UnityEvent<Entity>
{
}

public delegate void ActionEffect(Entity user);

public class Action
{
    public List<ActionEffect> AEfList;

    public void Execute(Entity user)
    {
        foreach (ActionEffect aEffect in AEfList)
            aEffect(user);
    }
}

public class CombatSysMgr : MonoBehaviour
{
    public Dictionary<string, Action> actionDic;
    public Dictionary<string, ActionEvent> actionEventDic;

    private static CombatSysMgr combatMgr;

    public static CombatSysMgr instance
    {
        get
        {
            if(!combatMgr)
            {
                combatMgr = FindObjectOfType(typeof(CombatSysMgr)) as CombatSysMgr;
            }

            if (!combatMgr)
            {
                Debug.LogError("There needs to be one active EventManger script on a GameObject in your scene.");
            }
            else
            {
                combatMgr.Init();
            }
            return combatMgr;
        }
    }

    private void Init()
    {
        if (actionDic == null)
            actionDic = new Dictionary<string, Action>();

        if (actionEventDic == null)
            actionEventDic = new Dictionary<string, ActionEvent>();
    }

    public ActionEvent GetActionEvent(string eventname)
    {
        foreach (KeyValuePair<string, ActionEvent> entry in actionEventDic)
        {
            if (entry.Key == eventname)
                return entry.Value;
        }
        return null;
    }

    public void TriggerAction(string ActionName, GameObject user)
    {
        foreach (KeyValuePair<string, Action> entry in actionDic)
        {
            if (entry.Key == ActionName)
                entry.Value.Execute(user.GetComponent<Entity>());
        }
    }

    public float TriggerActionEvent(string AEname, Entity user)
    {
        float value = 0;
        foreach (KeyValuePair<string, ActionEvent> entry in actionEventDic)
        {
            if (entry.Key == AEname)
                entry.Value.Invoke(user);
        }

        return value;
    }
}

