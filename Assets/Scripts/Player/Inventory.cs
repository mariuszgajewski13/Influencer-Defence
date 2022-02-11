using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] protected Dictionary<int, Carriable> inventory;

    public List<Carriable> tempInventory;

    private float swapRatio = 0.1f;
    private float sinceLastSwap = 1f;

    void Start()
    {
        inventory = new Dictionary<int, Carriable>();
        foreach (var t in tempInventory)
        {
            AddToInventory(t);
        }

        inventory.First().Value.gameObject.SetActive(true);
    }

    private void Update()
    {
        sinceLastSwap += Time.deltaTime;
    }

    void AddToInventory(Carriable c)
    {
        var obj = Instantiate(c, transform, false);
        inventory.Add(inventory.Count, obj);
        obj.gameObject.SetActive(false);
    }

    public void NextItem()
    {
        if (sinceLastSwap < swapRatio) return;
        int active = ActiveItem();
        
        Debug.Log(active);

        if (active == -1) return;

        inventory[active].gameObject.SetActive(false);
        inventory[(active >= inventory.Count - 1 ? 0 : active + 1)].gameObject.SetActive(true);

        sinceLastSwap = 0;

        
    }

    public void PreviousItem()
    {
        if (sinceLastSwap < swapRatio) return;
        int active = ActiveItem();

        if (active == -1) return;

        inventory[active].gameObject.SetActive(false);
        inventory[(active <= 0 ? inventory.Count - 1 : active - 1)].gameObject.SetActive(true);

        sinceLastSwap = 0;
    }

    public void SelectItem(int n)
    {
        if (sinceLastSwap < swapRatio) return;
        if (!inventory.ContainsKey(n - 1)) return;
        if (n == 0) n = 10;

        int active = ActiveItem();

        if (active == -1 || active == n - 1) return;
        inventory[active].gameObject.SetActive(false);

        inventory[n-1].gameObject.SetActive(true);

        sinceLastSwap = 0;
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
