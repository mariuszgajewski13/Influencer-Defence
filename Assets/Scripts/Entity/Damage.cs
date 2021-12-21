using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    float damage;
    float radius; // used for explosives
    float force; // used for penetrating shots, represented as % of armor penetrated
    DamageType damageType;

    public Damage(float damage, float radius = 0, float force = 1, DamageType damageType = DamageType.NORMAL)
    {
        this.damage = damage;
        this.radius = radius;
        this.force = force;
        this.damageType = damageType;
    }

    public float CalculateDamage(float armor = 0)
    {
        if (damageType == DamageType.PENETRATING)
        {
            armor *= Mathf.Clamp(force, 0, 1);
        }
        
        if (damageType == DamageType.EXPLOSIVE)
        {
            armor = 0;
        }
        
        float calculatedDamage = damage * (100 / (100 + armor));             

        return calculatedDamage;
    }
}

public enum DamageType
{
    NORMAL,
    EXPLOSIVE,
    PENETRATING
}

