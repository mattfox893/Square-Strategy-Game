using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    Tile tile;
    public string name;
    public Stats unitStats;
    [SerializeField] public Team team;
    public UnitState state;
    Vector2 gridPos;
    int currHealth, currSpeed, currStrength, currMagic, currRange, currDefense, currResilience, currMovement;
    //Animator animator;
    public Inventory inventory;

    void Awake() 
    {
        InitStats();
        state = UnitState.NotActed;
        gridPos = GetGridPos();
        tile = GetTile();
        tile.SetAttribute(Attribute.Impassable);
        inventory = this.GetComponent<Inventory>();
        //animator = this.GetComponent<Animator>();
    }

    void Update()
    {
        gridPos = GetGridPos();
        tile = GetTile();
        if (UnitSelection.selected != this)
        {
            tile.SetAttribute(Attribute.Impassable);
        }
    }

    void OnMouseDown() 
    {
        UnitSelection.SetSelected(this);
    }

    void OnMouseEnter() 
    {
        tile = GetTile();
        tile.EnableHighlight();
    }

    void OnMouseExit() 
    {
        tile = GetTile();
        tile.DisableHighlight();
    }

    

    public Inventory GetInventory()
    {
        return inventory;
    }

    void InitStats() 
    {
        currHealth = unitStats.Health;
        currSpeed = unitStats.Speed;
        currStrength = unitStats.Strength;
        currMagic = unitStats.Magic;
        currRange = unitStats.Range;
        currDefense = unitStats.Defense;
        currResilience = unitStats.Resilience;
        currMovement = unitStats.Movement;
    }

    public Tile GetTile()
    {
        return GridManager.GetTile(gridPos, tile);
    }

    // Subtracts remaining movement of the current Unit by
    // dis
    public void MoveUnit(int dis) 
    {
        currMovement -= dis;
    }

    public int GetMovement() 
    {
        return currMovement;
    }

    public int GetHealth() 
    {
        return currHealth;
    }

    public int GetDefense() 
    {
        return currDefense;
    }

    private int GetSpeed()
    {
        return currSpeed;
    }

    public Vector2 GetGridPos()
    {
        return new Vector2(transform.position.x, transform.position.z);
    }


    // amount is the raw damage,
    // isMagic asks if the damage is from a magic or physical weapon
    public void Damage(int amount, bool isMagic) 
    {
        int actualDamage = (amount - (isMagic ? currResilience : currDefense));
        
        currHealth -= actualDamage;

        if (currHealth <= 0) 
        {
            Death();
        }
    }

    public void Use(Item item)
    {
        inventory.UseItem(item);
    }

    public void AddStrength(int amt)
    {
        currStrength += amt;
    }

    public void AddSpeed(int amt)
    {
        currSpeed += amt;
    }

    public void AddMagic(int amt)
    {
        currMagic += amt; //Subject to change, not sure how strong enemies are
    }

    public void AddDefense(int amt)
    {
        currDefense += amt;
    }

    public void Heal(int amt)
    {
        if (currHealth <= unitStats.Health - 5)
        {
            currHealth += amt;
        }
        else
        {
            currHealth = unitStats.Health;
        }
    }

    // target is the Unit that the current Unit is attacking.
    public void Attack(Unit target)
    {
        //animator.SetBool("attacked", true);
        bool isMagic = false;
        int damage = 0;
        int numAttacks = NumAttacks(target);
        Debug.Log($"num attacks: {numAttacks}");

        if (inventory.HasEquipped())
        {
            isMagic = inventory.GetEquippedType();
            damage += inventory.GetEquippedDamage();
        }

        damage += isMagic ? currMagic : currStrength;
        
        for(int i = 0; i < numAttacks; i++)
        {
            target.Damage(damage, isMagic);
        }

        //animator.SetBool("attacked", false);
    }

    // The number of attacks to make against target based on the speed calculation.
    public int NumAttacks(Unit target)
    {
        int attacks = 1;

        if (currSpeed >= target.GetSpeed() + 3)
            attacks = (int) Mathf.Floor((currSpeed - target.GetSpeed()) / 3);
        /*if (attacks > 1000)
            attacks = 1000;*/

        return attacks;
    }


    // The current Unit will call this to see if they are in range to hit
    // target
    public bool inRange(Unit target)
    {
        // get a float of the distance between the interacting Units
        float rangeWithin = Vector2.Distance(target.GetGridPos(), gridPos);
        // return whether the target is within the current Unit's range and not too close/far
        return (rangeWithin <= currRange && rangeWithin > currRange - 1);
    }

    // call this only when the Unit's health is reduced to 0 or they are instantly killed otherwise.
    void Death() 
    {
        //animator.SetBool("hasDied", true);
        tile = GetTile();
        tile.SetAttribute(Attribute.Normal);
        Destroy(this.gameObject);
    }

    public void StartTurn()
    {
        tile = GetTile();
        tile.SetAttribute(Attribute.Impassable);
        currMovement = unitStats.Movement;
        state = UnitState.NotActed;
    }

    public void EndTurn()
    {
        tile = GetTile();
        tile.SetAttribute(Attribute.Impassable);
        currMovement = 0;
        state = UnitState.Acted;
    }


    
    /*switch (item)
       {
           case "Mana":
               currMagic += 4; //Subject to change, depends on how powerful the enemies are/the unit is
               break;
           case "Strength":
               currStrength += 4;
               break;
           case "Vitality":
               if (currHealth <= unitStats.Health - 5)
                   currHealth += 5;
               else
                   currHealth += (unitStats.Health - currHealth);
               currDefense += 5;
               break;
           case "Stamina":
               currSpeed += 1;
               currMovement += 1;
               break;
       }*/
    /*void OnCollisionEnter(Collision collided)
    {
        switch (collided.gameObject.name)
        {
            case "blue_pot":
                if (potions.Item2 < potions.Item1.Length)
                {
                    potions.Item1[potions.Item2] = "Mana";
                    potions.Item2 += 1;
                    Destroy(collided.gameObject);
                }
                break;
            case "red_pot":
                if (potions.Item2 < potions.Item1.Length)
                {
                    potions.Item1[potions.Item2] = "Strength";
                    potions.Item2 += 1;
                    Destroy(collided.gameObject);
                }
                break;
            case "green_pot":
                if (potions.Item2 < potions.Item1.Length)
                {
                    potions.Item1[potions.Item2] = "Vitality";
                    potions.Item2 += 1;
                    Destroy(collided.gameObject);
                }
                break;
            case "yellow_pot":
                if (potions.Item2 < potions.Item1.Length)
                {
                    potions.Item1[potions.Item2] = "Stamina";
                    potions.Item2 += 1;
                    Destroy(collided.gameObject);
                }
                break;
        }
    }*/
}

public enum Team
{
    Ally = 0,
    Enemy = 1
}

public enum UnitState
{
    NotActed = 0,
    Acted = 1,
    ActReady = 2
}