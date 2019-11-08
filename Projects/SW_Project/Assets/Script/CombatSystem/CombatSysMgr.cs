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
    public static Dictionary<string, Action> actionDic;
    public static Dictionary<string, ActionEvent> actionEventDic;

    private void Awake()
    {
        if (actionDic == null)
            actionDic = new Dictionary<string, Action>();

        if (actionEventDic == null)
            actionEventDic = new Dictionary<string, ActionEvent>();
    }


    public static ActionEvent GetActionEvent(string eventname)
    {
        foreach (KeyValuePair<string, ActionEvent> entry in actionEventDic)
        {
            if (entry.Key == eventname)
                return entry.Value;
        }
        return null;
    }

    public static void TriggerAction(string ActionName, GameObject user)
    {
        foreach (KeyValuePair<string, Action> entry in actionDic)
        {
            if (entry.Key == ActionName)
                entry.Value.Execute(user.GetComponent<Entity>());
        }
    }

    public static float TriggerActionEvent(string AEname, Entity user)
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

