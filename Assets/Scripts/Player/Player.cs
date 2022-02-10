using UnityEngine;
using UnityEngine.UI;

public class Player : EntityBase
{
    void Start()
    {
        SetHealth();
        slider = GameObject.Find("/UI/Health Bar").GetComponent<Slider>();

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            TakeDamage(20);
        }
    }

    private void Update()
    {
        UpdateHealthBar();
    }

    override protected void Die()
    {
        FindObjectOfType<GameManager>().GameOver();
    }
}
