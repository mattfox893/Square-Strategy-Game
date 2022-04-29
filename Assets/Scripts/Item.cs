using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public string name;
    public ItemType type;

    public Item(string name, ItemType type)
    {
        this.name = name;
        this.type = type;
    }

    public Item(Item item)
    {
        this.name = item.name;
        this.type = item.type;
    }

    /*public string Use()
    {
        string str = this.name;

    }*/
}

public enum ItemType
{
    Weapon = 0,
    Potion = 1
}
