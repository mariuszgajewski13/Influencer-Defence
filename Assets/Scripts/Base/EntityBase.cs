using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityBase : MonoBehaviour
{
    [SerializeField] protected bool hasAI;
    [SerializeField] protected float maxHP;
    [SerializeField] protected int movementSpeed;
    [SerializeField] protected float armor;  
    
    protected float currentHP;

    virtual public float GetHealth()
    {
        return currentHP;
    }

    virtual public void AddHealth(float amount)
    {
        currentHP = Mathf.Clamp(amount + currentHP, 0, maxHP);
    }

    virtual public void RemoveHealth(Damage damage)
    {
        currentHP -= damage.CalculateDamage(armor);

        if (currentHP <= 0f) EntityDeath();
    }

    virtual public void EntityDeath()
    {
        Destroy(gameObject);
    }

}
