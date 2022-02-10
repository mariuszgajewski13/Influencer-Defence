using UnityEngine;

public class WeaponSwap : MonoBehaviour
{
    private Inventory inventory;
    void Start()
    {
        inventory = GetComponent<Inventory>();
    }

    void FixedUpdate()
    {
        SwapWeapons();
    }

    // Functions getting called twice
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
