using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] protected Dictionary<int, Carriable> inventory;

    public List<Carriable> tempInventory;

    void Start()
    {
        inventory = new Dictionary<int, Carriable>();
        foreach (var t in tempInventory)
        {
            AddToInventory(t);
        }

        inventory.First().Value.gameObject.SetActive(true);
    }

    void AddToInventory(Carriable c)
    {
        var obj = Instantiate(c, c.gameObject.transform.position, c.gameObject.transform.rotation, transform);
        inventory.Add(inventory.Count, obj);
        obj.gameObject.SetActive(false);
    }

    public void NextItem()
    {
        int active = ActiveItem();

        if (active == -1) return;

        inventory[active].gameObject.SetActive(false);
        inventory[(active >= inventory.Count - 1 ? 0 : active + 1)].gameObject.SetActive(true);

    }

    public void PreviousItem()
    {
        int active = ActiveItem();

        if (active == -1) return;

        inventory[active].gameObject.SetActive(false);
        inventory[(active <= 0 ? inventory.Count - 1 : active - 1)].gameObject.SetActive(true);
        

    }

    public void SelectItem(int n)
    {
        if (!inventory.ContainsKey(n)) return;

        int active = ActiveItem();

        if (active == -1 || active == n - 1) return;
        inventory[active].gameObject.SetActive(false);

        inventory[n].gameObject.SetActive(true);
        
    }

    int ActiveItem()
    {
        foreach (var i in inventory)
        {
            if (i.Value.gameObject.activeSelf) return i.Key;
        }

        return -1;
    }
}
