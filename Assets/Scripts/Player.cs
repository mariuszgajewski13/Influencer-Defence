using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealt;

    public HealthBar healthBar;

    void Start()
    {
        currentHealt = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            TakeDamage(20);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealt -= damage;
        healthBar.SetHealt(currentHealt);
    }

    private void Update()
    {
        if(currentHealt <= 0)
        {
            FindObjectOfType<GameManager>().GameOver();
        }
    }
}
