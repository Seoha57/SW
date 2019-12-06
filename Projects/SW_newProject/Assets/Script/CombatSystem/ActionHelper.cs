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
            calculated_damage = calculated_damage - Mathf.Min(calculated_damage, (e.attribute.Damage * e.target.GetComponent<Entity>().attribute.Armor / 100.0f));
        }
        return calculated_damage;
    }

    public float CoolTimeMod(Entity e, float coolTime)
    {
        float mod_cool = coolTime;

        mod_cool -= coolTime * (e.attribute.Speed / 100.0f);
        if (mod_cool < 0)
            mod_cool = 0;

        return mod_cool;
    }

    public float HealPointMod(Entity e)
    {
        float point = 5 + 5 * (e.attribute.Mana / 10.0f);

        return point;
    }
}
