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
    public Vector2 startPos;
    Vector2 gridPos;
    public int maxHealth, currHealth, currSpeed, currStrength, currMagic, currRange, currDefense, currResilience, currMovement, maxMovement;
    public Animator animator;
    public AudioSource phys, mag, death;
    Inventory inventory;

    void Awake() 
    {
        InitStats();
        state = UnitState.NotActed;
        gridPos = GetGridPos();
        tile = GetTile();
        tile.SetAttribute(Attribute.Impassable);
        inventory = this.GetComponent<Inventory>();
        animator = this.GetComponent<Animator>();
        startPos = GetGridPos();
        if (team == Team.Ally)
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    void Update()
    {
        gridPos = GetGridPos();
        tile = GetTile();
        if (UnitSelection.Instance.selected != this)
        {
            tile.SetAttribute(Attribute.Impassable);
        }
    }

    void OnMouseDown() 
    {
        UnitSelection.Instance.SetSelected(this);
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

    public void InitStats() 
    {
        maxHealth = unitStats.Health;
        currHealth = maxHealth;
        currSpeed = unitStats.Speed;
        currStrength = unitStats.Strength;
        currMagic = unitStats.Magic;
        currRange = unitStats.Range;
        currDefense = unitStats.Defense;
        currResilience = unitStats.Resilience;
        maxMovement = unitStats.Movement;
        currMovement = maxMovement;
    }

    public Tile GetTile()
    {
        return GridManager.GetTile(gridPos, tile);
    }

    // Subtracts remaining movement of the current Unit by
    // dis
    public void MoveUnit(int dis) 
    {
        if (currMovement - dis < 0)
            currMovement = 0;
        else
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
        
        bool isMagic = false;
        int damage = 0;
        int numAttacks = NumAttacks(target);

        // add stats of weapon if one is equipped
        if (inventory.HasEquipped())
        {
            isMagic = inventory.GetEquippedType();
            damage += inventory.GetEquippedDamage();
        }
        
        if (isMagic){
            animator.Play("Base Layer.Mag");
        }
        else
        {
            animator.Play("Base Layer.Phys");
        }

        // turn to look at the target
        transform.LookAt(target.transform);

        // add base damage
        damage += isMagic ? currMagic : currStrength;
        
        // attack a number of times determined by the calculation
        for(int i = 0; i < numAttacks; i++)
        {
            target.Damage(damage, isMagic);
            if (isMagic)
            {
                mag.Play();
            }
            else
            {
                phys.Play();
            }
        }
         
    }

    // The number of attacks to make against target based on the speed calculation.
    public int NumAttacks(Unit target)
    {
        int attacks = 1;

        // for every 3 speed greater than the target, attack another time
        if (currSpeed >= target.GetSpeed() + 3)
            attacks = (int) Mathf.Floor((currSpeed - target.GetSpeed()) / 3);

        return attacks;
    }


    // The current Unit will call this to see if they are in range to hit
    // target
    public bool InRange(Unit target)
    {
        // get a float of the distance between the interacting Units
        gridPos = GetGridPos();
        float rangeWithin = Vector2.Distance(target.GetGridPos(), gridPos);
        // return whether the target is within the current Unit's range and not too close/far
        return (rangeWithin <= currRange && rangeWithin > currRange - 1);
    }

    // call this only when the Unit's health is reduced to 0 or they are instantly killed otherwise.
    void Death() 
    {
        if (this.team == Team.Ally)
            animator.Play("Death");
        else
            animator.SetBool("hasDied", true);
        tile = GetTile();
        tile.SetAttribute(Attribute.Normal);

        // remove from Unit Manager
        UnitManager.Instance.units.Remove(this);

        if (team == Team.Ally)
        {
            UnitManager.Instance.allies.Remove(this);
        } 
        else if (team == Team.Enemy)
        {
            UnitManager.Instance.enemies.Remove(this);
        }

        UnitManager.Instance.CheckTeamDefeated(team);

        death.Play();

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
        UnitManager.Instance.CheckTeamStatus(Team.Ally);
        UnitSelection.Instance.Deselect();
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