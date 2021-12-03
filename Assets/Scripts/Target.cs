using UnityEngine;

public class Target : MonoBehaviour
{
    public float hp = 50f;

    public void TakeDamege(float amount)
    {
        hp -= amount;
        if(hp <= 0f)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
