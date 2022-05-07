using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    List<Item> items;
    Unit unit;
    Item equipped;

    private void Start()
    {
        items = new List<Item>();
        unit = this.GetComponent<Unit>();
        equipped = null;
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

    public void UseItem(int i)
    {
        UseItem(items[i]);
    }

    public bool UseItem(Item item)
    {
        if (item == null)
            return false;

        if (RemoveItem(item))
        {
            item.Use(unit);
            return true;
        }

        return false;
    }

    public List<Item> GetItems()
    {
        return items;
    }

    public bool HasEquipped()
    {
        return equipped != null;
    }

    public void Equip(Item item)
    {
        equipped = item;
    }

    // return damage of equipped item
    public int GetEquippedDamage()
    {
        return equipped.damage;
    }

    // returns true if the equipped item is magic
    public bool GetEquippedType()
    {
        return equipped.isMagic;
    }
}
