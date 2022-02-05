using UnityEngine;

public class Target : MonoBehaviour
{
    public int enemyMaxHp = 50;
    public int enemyHp;
    public GameObject blood;
    public EnemyHealthBar enemyHealthBar;

    public void Start()
    {
        enemyHp = enemyMaxHp;
        //enemyHealthBar.SetMaxHealth(enemyMaxHp);
        blood = GameObject.FindGameObjectWithTag("Blood");
    }

    public void TakeDamage(int amount)
    {
        enemyHp -= amount;
        enemyHealthBar.SetHealt(enemyHp);

        //Instantiate(blood);

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
