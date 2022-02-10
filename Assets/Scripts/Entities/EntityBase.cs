using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EntityBase : MonoBehaviour
{
    public float maxHP = 100;
    protected float currentHP;

    public GameObject bloodEffect;
    public GameObject impactEffect;
    public bool organic = true;

    public AudioSource damagedSound;
    public AudioSource deathSound;

    [SerializeField] protected Slider slider;
    [SerializeField] protected TextMeshProUGUI healthUI;

    void Start()
    {
        SetHealth();
    }

    public void TakeDamage(float damage, RaycastHit r = new RaycastHit(), float impactForce = 0f)
    {
        currentHP -= damage;
        if (organic)
        {
            GameObject blood = Instantiate(bloodEffect, r.point, Quaternion.FromToRotation(Vector3.up, r.normal));
            blood.transform.SetParent(transform);
            damagedSound.Play();
            Destroy(blood, 2f);
        }

        if (r.rigidbody != null)
        {
            r.rigidbody.AddForce(-r.normal * impactForce);
        }

        GameObject impact = Instantiate(impactEffect, r.point, Quaternion.FromToRotation(Vector3.up, r.normal));
        Destroy(impact, 0.5f);

        if (currentHP <= 0)
        {
            deathSound.Play();
            Die();
        }
    }

    virtual protected void Die()
    {
        Destroy(gameObject);
    }

    protected void SetHealth()
    {
        currentHP = maxHP;
    }

    protected void UpdateHealthBar()
    {
        if (slider != null)
            slider.value = currentHP / maxHP;
        if (healthUI != null)
            healthUI.text = $"{currentHP}/{maxHP}";
    }

}
