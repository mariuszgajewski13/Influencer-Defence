using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealt;

    public HealthBar healthBar;

    // Start is called before the first frame update
    void Start()
    {
        currentHealt = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    private void OnTriggerEnter(Collider other)
    {
        TakeDamage(20);
    }

    public void TakeDamage(int damage)
    {
        currentHealt -= damage;
        healthBar.SetHealt(currentHealt);
    }
}
