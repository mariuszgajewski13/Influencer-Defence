using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ADS : MonoBehaviour
{
    public Vector3 aimDownSight;
    public Vector3 hipFire;

    public GameObject weapon;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log("ADS");
            weapon.gameObject.transform.position = new Vector3(0.012f, -0.292f, 0.738f);
        }
        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            weapon.transform.position = hipFire;
        }
    }
}
