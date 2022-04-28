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

    void Start() 
    {
        InitStats();
        state = UnitState.NotActed;
        gridPos = GetGridPos();
        tile = GetTile();
        tile.SetAttribute(Attribute.Impassable);
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