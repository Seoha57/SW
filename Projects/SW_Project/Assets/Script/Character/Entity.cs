using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public int ID;
    public float HP;

    public Attributes attribute;

    public GameObject target;

    void Start()
    {
        ActionEvent Physical = new ActionEvent();
        if (CombatSysMgr.instance.actionEventDic.TryGetValue("TakePhysicalDamage", out Physical))
        {
            Physical.AddListener(TakeDamage);
        }
    }

    void TakeDamage(Entity e)
    {
        if (e.target.GetComponent<Entity>().ID == this.ID)
        {
            this.HP -= Actions.GetPhysicalDamage();
            if (this.HP <= 0)
                this.gameObject.SetActive(false);
        }
    }
}
