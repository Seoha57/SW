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
            Debug.Log(calculated_damage);
            calculated_damage = calculated_damage - Mathf.Min(calculated_damage, (e.attribute.Damage * e.target.GetComponent<Entity>().attribute.Armor / 100.0f));
        }
        return calculated_damage;
    }

    public float CoolTimeMod(Entity e, float coolTime)
    {
        float mod_GCD = coolTime;

        mod_GCD -= coolTime * (e.attribute.Speed / 100.0f);

        return mod_GCD;
    }
}
