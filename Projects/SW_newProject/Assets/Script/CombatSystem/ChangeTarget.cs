using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeTarget : MonoBehaviour
{
    Actions action;
    Entity Player;

    private void Start()
    {
        action = GetComponent<Actions>();
        Player = action.player[0].GetComponent<Entity>();

        ActionEvent check = null;
        CombatSysMgr.instance.actionEventDic.TryGetValue("TargetIsChanged", out check);
        if (check == null)
            CombatSysMgr.instance.actionEventDic.Add("TargetIsChanged", new ActionEvent());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.name == "Enemy1")
                {
                    Player.target.transform.GetChild(0).gameObject.SetActive(false);
                    Player.target = action.enemy[0];
                    CombatSysMgr.instance.TriggerActionEvent("TargetIsChanged", action.player[0].GetComponent<Entity>());
                }
                if (hit.transform.name == "Enemy2")
                {
                    Player.target.transform.GetChild(0).gameObject.SetActive(false);
                    Player.target = action.enemy[1];
                    CombatSysMgr.instance.TriggerActionEvent("TargetIsChanged", action.player[0].GetComponent<Entity>());
                }
                if (hit.transform.name == "Enemy3")
                {
                    Player.target.transform.GetChild(0).gameObject.SetActive(false);
                    Player.target = action.enemy[2];
                    CombatSysMgr.instance.TriggerActionEvent("TargetIsChanged", action.player[0].GetComponent<Entity>());
                }
            }
        }
    }
}
