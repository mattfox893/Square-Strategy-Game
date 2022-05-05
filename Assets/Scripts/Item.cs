using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public string name;
    public ItemType type;
    public PotionType potType;
    public bool isMagic;
    public int damage;

    // use this constructor if the item is a potion
    public Item(string name, PotionType potType)
    {
        this.name = name;
        this.type = ItemType.Potion;
        this.isMagic = false;
        this.potType = potType;
        this.damage = 0;
    }

    // use this constructor if the item is a weapon
    public Item(string name, bool isMagic, int damage)
    {
        this.name = name;
        this.type = ItemType.Weapon;
        this.isMagic = isMagic;
        this.damage = damage;
        this.potType = PotionType.NotPotion;
    }

    // copy constructor
    public Item(Item item)
    {
        this.name = item.name;
        this.type = item.type;
        this.isMagic = item.isMagic;
        this.potType = item.potType;
    }

    public void Use(Unit owner)
    {
        if (type == ItemType.Weapon)
        {
            EquipWeapon(owner);
        }
            
        if (type == ItemType.Potion)
        {
            UsePotion(owner);
        }

    }

    void EquipWeapon(Unit owner)
    {
        owner.GetInventory().Equip(this);
    }


    void UsePotion(Unit owner)
    {
        switch(potType)
        {
            case PotionType.Mana:
                owner.AddMagic(3);
                break;
            case PotionType.Vitality:
                owner.Heal(5);
                break;
            case PotionType.Stamina:
                owner.AddSpeed(3);
                break;
            case PotionType.Strength:
                owner.AddStrength(3);
                break;
            default:
                Debug.Log("ERROR! Not a potion!");
                break;
        }
    }
}

public enum ItemType
{
    Weapon = 0,
    Potion = 1
}

public enum PotionType
{
    NotPotion = 99,
    Mana = 0,
    Vitality = 1,
    Stamina = 2,
    Strength = 3
}
