using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwap : MonoBehaviour
{
    public int weaponID = 0;
    
    void Start()
    {
        SelectWeapon();
    }

    void Update()
    {
        int previousWeapon = weaponID;

        if(Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            if (weaponID >= transform.childCount - 1)
                weaponID = 0;
            else
                weaponID++;
        }

        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            if (weaponID <= 0)
                weaponID = transform.childCount - 1;
            else
                weaponID--;
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            weaponID = 0;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) && transform.childCount >=2)
        {
            weaponID = 1;
        }

        if (previousWeapon != weaponID)
        {
            SelectWeapon();
        }
    }

    void SelectWeapon()
    {
        int i = 0;
        foreach(Transform weapon in transform)
        {
            if (i == weaponID)
                weapon.gameObject.SetActive(true);
            else
                weapon.gameObject.SetActive(false);
            i++;
        }
    }
}
