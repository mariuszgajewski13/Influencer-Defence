using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Weapon : MonoBehaviour
{
    public int damage = 10;
    public float range = 100f;
    public float impactForce = 30f;
    public float fireRate = 15f;
    public int bulletsPerShot = 1;
    public float spread = 0.01f;

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

    public AudioSource shotSound;
    public AudioSource reloadSound;

    void OnEnable()
    {
        UpdateAmmoText();
        isReloading = false;
        animator.SetBool("Reloading", false);
        currentAmmo = maxAmmo;
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
            StartCoroutine(ReloadCoroutine());
            reloadSound.Play();
            return;
        }
    }

    IEnumerator ReloadCoroutine()
    {
        isReloading = true;
        Debug.Log("Reloading...");

        animator.SetBool("Reloading", true);

        yield return new WaitForSeconds(reloadTime - .25f);
        animator.SetBool("Reloading", false);
        yield return new WaitForSeconds(.25f);

        currentAmmo = maxAmmo;
        UpdateAmmoText();
        isReloading = false;
    }

    private void Shoot()
    {
        muzzleFlash.Play();
        muzzleFlash.GetComponent<AudioSource>().Play();
        shotSound.Play();
        currentAmmo--;

        UpdateAmmoText();

        var casts = SendRandomRaycasts(bulletsPerShot);

        foreach(var c in casts)
        {
            if (!c.Value) continue;

            EntityBase target = c.Key.transform.GetComponent<EntityBase>();

            if (target != null)
            {
                target.TakeDamage(damage, c.Key, impactForce);
            }

            if (c.Key.rigidbody != null)
            {
                c.Key.rigidbody.AddForce(-c.Key.normal * impactForce);
            }

            GameObject impact = Instantiate(impactEffect, c.Key.point, Quaternion.FromToRotation(Vector3.up, c.Key.normal));
            Destroy(impact, 0.5f);
        }
    }

    // Useful for shotguns
    private Dictionary<RaycastHit, bool> SendRandomRaycasts(int numberOfCasts)
    {
        Dictionary<RaycastHit, bool> raycasts = new Dictionary<RaycastHit, bool>();

        for (int i = 0; i < numberOfCasts; i++)
        {
            RaycastHit raycast;
            Vector3 randomPoint = Random.insideUnitCircle * spread;
            bool gotHit = Physics.Raycast(fpsCam.transform.position, (fpsCam.transform.forward + randomPoint).normalized, out raycast, range);
            raycasts.Add(raycast, gotHit);
        }

        return raycasts;
    }

    public void UpdateAmmoText()
    {
        ammoCount.text = currentAmmo.ToString();
    }

}
