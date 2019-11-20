using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AttackType { PHYSICAL, ELEMENTAL };

public class ActionHelper : MonoBehaviour
{
    public float DamageMod(Entity e, AttackType type)
    {
        float calculated_damage = 0;

        if (type == AttackType.PHYSICAL)
        {
            calculated_damage = e.attribute.Damage;
            //calculated_damage = IsCriticalHit(e, calculated_damage);
            calculated_damage = calculated_damage - (e.attribute.Damage * e.target.GetComponent<Entity>().attribute.Armor / 100.0f);
        }
        //if (type == AttackType.ELEMENTAL)
        //{
        //    calculated_damage = e.attribute.ElemetalDamage;
        //    calculated_damage = IsCriticalHit(e, calculated_damage);
        //    calculated_damage = calculated_damage - (e.attribute.PhysicalDamage * e.target.GetComponent<Entity>().attribute.AfflictionResistance / 100.0f);
        //}

        return calculated_damage;
    }

    //private float IsCriticalHit(Entity e, float damage)
    //{
    //    if (Random.Range(0, 100.0f) <= e.attribute.CriticalChance)
    //    {
    //        Debug.Log("Critical Hit!!");
    //        damage = damage + damage * (e.attribute.CriticalDamage / 100.0f);
    //    }

    //    return damage;
    //}

    public float CoolTimeMod(Entity e, float coolTime)
    {
        float mod_GCD = coolTime;

        mod_GCD -= coolTime * (e.attribute.Speed / 100.0f);

        return mod_GCD;
    }
}
