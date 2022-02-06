using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class Shooting : MonoBehaviour
{
    public int damage = 10;
    public float range = 100f;
    public float impactForce = 30f;
    public float fireRate = 15f;

    public int maxAmmo = 10;
    public int currentAmmo;
    public float reloadTime = 1f;
    private bool isReloading = false;
    public Animator animator;

    public Camera fpsCam;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;
    public GameObject bloodEffect;

    private float nextTimeToFire = 0f;

    public TextMeshProUGUI ammoCount;

    private void Start()
    {
        currentAmmo = maxAmmo;
        UpdateAmmoText();
    }

    void OnEnable()
    {
        isReloading = false;
        animator.SetBool("Reloading", false);
    }

    void Update()
    {
        if (isReloading)
            return;

        if (Input.GetKey(KeyCode.Mouse0) && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
        }

        if (currentAmmo <= 0 || (Input.GetKey(KeyCode.R) && currentAmmo != maxAmmo))
        {
            StartCoroutine(Reload());
            return;
        }
    }

    IEnumerator Reload()
    {
        isReloading = true;
        Debug.Log("Reloading...");

        animator.SetBool("Reloading", true);

        yield return new WaitForSeconds(reloadTime -.25f);
        animator.SetBool("Reloading", false);
        yield return new WaitForSeconds(.25f);

        currentAmmo = maxAmmo;
        UpdateAmmoText();
        isReloading = false;
    }

    public void Shoot()
    {
        muzzleFlash.Play();
        muzzleFlash.GetComponent<AudioSource>().Play();
        currentAmmo--;

        UpdateAmmoText();

        RaycastHit hit;
        if(Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);

            Target target = hit.transform.GetComponent<Target>();

            if(target != null)
            {
                target.TakeDamage(damage);
                
                GameObject blood = Instantiate(bloodEffect, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal));
                blood.transform.SetParent(target.transform);
                if(target.enemyHp <= 0)
                    Destroy(blood);
            }

            if(hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * impactForce);
            }

            GameObject impact =  Instantiate(impactEffect, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal));
            Destroy(impact, 0.5f);
        }
    }

    public void UpdateAmmoText()
    {
        ammoCount.text = currentAmmo.ToString();
    }
}
