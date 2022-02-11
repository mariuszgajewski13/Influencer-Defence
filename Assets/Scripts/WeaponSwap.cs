using UnityEngine;

public class WeaponSwap : MonoBehaviour
{
    private Inventory inventory;
    void Start()
    {
        inventory = GetComponent<Inventory>();
    }

    void Update()
    {
        SwapWeapons();
    }
    void SwapWeapons()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            inventory.NextItem();
        }

        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            inventory.PreviousItem();
        }

        for (int i = 0; i < 10; i++)
        {
            if (Input.GetKeyDown("" + i))
            {
                inventory.SelectItem(i);
            }
        }


    }
}
