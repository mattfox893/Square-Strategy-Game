using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    List<Item> items;

    private void Start()
    {
        items = new List<Item>();
    }

    public void AddItem(Item item)
    {
        if (item == null)
            return;

        items.Add(item);
    }

    public void RemoveItem(Item item)
    {
        if (item == null)
            return;

        items.Remove(item);
    }

    public List<Item> GetItems()
    {
        return items;
    }
}
