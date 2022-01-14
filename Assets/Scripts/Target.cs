using UnityEngine;

public class Target : MonoBehaviour
{
    public int enemyMaxHp = 50;
    public int enemyHp;

    public EnemyHealthBar enemyHealthBar;

    public void Start()
    {
        enemyHp = enemyMaxHp;
        //enemyHealthBar.SetMaxHealth(enemyMaxHp);
    }

    public void TakeDamage(int amount)
    {
        enemyHp -= amount;
        //enemyHealthBar.SetHealt(enemyHp);

        if (enemyHp <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }

}
