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

    public bool RemoveItem(Item item)
    {
        if (item == null)
            return false;

        if (item.name == "Physical" || item.name == "Magical")
            return true;

        return items.Remove(item);
    }

    public string UseItem(Item item)
    {
        string str = item.name;
        if (RemoveItem(item))
        {
            return str;
        }
        return null;
    }

    public List<Item> GetItems()
    {
        return items;
    }
}
