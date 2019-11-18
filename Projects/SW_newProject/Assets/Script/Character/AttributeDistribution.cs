using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttributeDistribution : MonoBehaviour
{
    private Attributes point;
    private Attributes a;

    private void Start()
    {
        point = new Attributes();
        a = new Attributes
        {
            MaxHealth = 0.1f,
            Armor = 0.05f,
            Mana = 0.05f,
            Damage = 0.1f,
            Speed = 0.1f
        };
    }
    


    public float AD_MaxHealth(float init)
    {
        point.MaxHealth+=1;
        Debug.Log("MaxHealth Point is " + point.MaxHealth);
        return init+ (init * a.MaxHealth * point.MaxHealth);
    }
    public float AD_Armor(float init)
    {
        point.Armor+=1;
        Debug.Log("Armor Point is " + point.Armor);
        return init + (init * a.Armor * point.Armor);
    }
    public float AD_Mana(float init)
    {
        point.Mana+=1;
        Debug.Log("Mana Point is " + point.Mana);
        return init + (init * a.Mana * point.Mana);
    }
    public float AD_Damage(float init)
    {
        point.Damage+=1;
        Debug.Log("Armor Point is " + point.Damage);

        return init + (init * a.Damage * point.Damage);
    }
    public float AD_Speed(float init)
    {
        point.Speed+=1;
        Debug.Log("AD_Speed is " + point.Speed);
        return init + (init * a.Speed * point.Speed);
    }

    //#region Getter
    //public float AD_MaxHealth(float init)
    //{
    //    Debug.Log("MaxHealth Point is " + point_MaxHealth);
    //    return init + (init * MaxHealth * point_MaxHealth);
    //}
    //public float AD_Armor(float init)
    //{
    //    Debug.Log("Armor Point is " + point_Armor);
    //    return init + (init * Armor * point_Armor);
    //}
    //public float AD_ElementalWard(float init)
    //{
    //    Debug.Log("Elemental Ward Point is " + point_ElementalWard);
    //    return init + (init * ElementalWard * point_ElementalWard);
    //}
    //public float AD_AfflictionResistance(float init)
    //{
    //    Debug.Log("Affliction Resistance Point is " + point_AfflictionResistance);
    //    return init + (init * AfflictionResistance * point_AfflictionResistance);
    //}
    ////wis
    //public float AD_MaxResource(float init)
    //{
    //    Debug.Log("MaxResource Point is " + point_MaxResource);
    //    return init + (init * MaxResource * point_MaxResource);
    //}
    //public float AD_ResourceRegeneration(float init)
    //{
    //    Debug.Log("Resource Regeneration Point is " + point_ResourceRegeneration);
    //    return init + (init * ResourceRegeneration * point_ResourceRegeneration);
    //}
    //public float AD_ResourceCostReduction(float init)
    //{
    //    Debug.Log("Resource Cost Reduction Point is " + point_ResourceCostReduction);
    //    return init + (init * ResourceCostReduction * point_ResourceCostReduction);
    //}

    ////str
    //public float AD_ArmorBreak(float init)
    //{
    //    Debug.Log("ArmorBreak Point is " + point_ArmorBreak);
    //    return init + (init * ArmorBreak * point_ArmorBreak);
    //}
    //public float AD_PhysicalDamage(float init)
    //{
    //    Debug.Log("PhysicalDamage Point is " + point_PhysicalDamage);
    //    return init + (init * PhysicalDamage * point_PhysicalDamage);
    //}
    //public float AD_SovereignDamage(float init)
    //{
    //    Debug.Log("SovereignDamae Point is " + point_SovereignDamae);
    //    return init + (init * SovereignDamae * point_SovereignDamae);
    //}

    ////dex
    //public float AD_CriticalChance(float init)
    //{
    //    Debug.Log("Critical Chance Point is " + point_CriticalChance);
    //    return init + (init * CriticalChance * point_CriticalChance);
    //}
    //public float AD_CriticalDamage(float init)
    //{
    //    Debug.Log("CriticalDamage Point is " + point_CriticalDamage);
    //    return init + (init * CriticalDamage * point_CriticalDamage);
    //}
    //public float AD_AttackSpeed(float init)
    //{
    //    Debug.Log("AttackSpeed Point is " + point_AttackSpeed);
    //    return init + (init * AttackSpeed * point_AttackSpeed);
    //}
    ////int
    //public float AD_ElemetalDamage(float init)
    //{
    //    Debug.Log("ElemetalDamage Point is " + point_ElemetalDamage);
    //    return init + (init * ElemetalDamage * point_ElemetalDamage);
    //}
    //public float AD_WardBreak(float init)
    //{
    //    Debug.Log("WardBreak Point is " + point_WardBreak);
    //    return init + (init * WardBreak * point_WardBreak);
    //}
    //public float AD_CastingSpeed(float init)
    //{
    //    Debug.Log("CastingSpeed Point is " + point_CastingSpeed);
    //    return init + (init * CastingSpeed * point_CastingSpeed);
    //}
    //#endregion
}
