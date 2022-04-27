using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    Tile tile;
    public string name;
    public PlayerMovement moveScript;
    public Stats unitStats;
    public bool selectable;
    Vector2 gridPos;
    int currHealth, currSpeed, currStrength, currMagic, currRange, currDefense, currResilience, currMovement;
    //Animator animator;

    void Start() 
    {
        InitStats();
        moveScript = this.GetComponent<PlayerMovement>();
        // currently how we determine factions (BAD!)
        selectable = true;
        if (moveScript == null)
            selectable = false;
        tile = GridManager.GetTile((new Vector2(transform.position.x, transform.position.z)), tile);
        //animator = this.GetComponent<Animator>();
    }

    void Update()
    {
        gridPos = new Vector2(transform.position.x, transform.position.z);
    }

    void OnMouseDown() 
    {
        UnitSelection.SetSelected((gridPos, this));
    }

    void OnMouseEnter() 
    {
        tile = GridManager.GetTile(gridPos, tile);
        tile.EnableHighlight();
    }

    void OnMouseExit() 
    {
        tile = GridManager.GetTile(gridPos, tile);
        tile.DisableHighlight();
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

    public Vector2 GetGridPos()
    {
        return gridPos;
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

    // target is the Unit that the current Unit is attacking.
    // isMagic asks if the attack is magical in nature, for the purposes
    // of determining damage.
    public void Attack(Unit target, bool isMagic)
    {
        //animator.SetBool("attacked", true);
        int damage = isMagic ? currMagic : currStrength;
        target.Damage(damage, isMagic);
        //animator.SetBool("attacked", false);
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
        GridManager.GetTile(gridPos, tile).SetAttribute(Attribute.Normal);
        Destroy(this.gameObject);
    }
}

public enum Team
{
    Ally = 0,
    Enemy = 1
}

public enum UnitState
{
    NotActed = 0,
    Acted = 1
}