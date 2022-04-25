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
    int currHealth, currSpeed, currStrength, currMagic, currRange, currDefense, currResilience, currMovement;

    void Start() {
        InitStats();
        selectable = true;
        moveScript = this.GetComponent<PlayerMovement>();
        if (moveScript == null)
            selectable = false;
        tile = CreateGrid.GetTile((new Vector2(transform.position.x, transform.position.z)), tile);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.transform.name == "Sample Enemy") {
                    Unit enemy = GameObject.Find(hit.transform.name).GetComponent<Unit>();
                    Debug.Log($"enemy = {enemy}");
                    Unit selected = UnitSelection.GetSelected().Item2;
                    if (selected != null && Vector3.Distance(enemy.transform.position, selected.transform.position) <= 1.42) {
                        enemy.Damage(currStrength, false);
                    }
                }
            }
        }
    }

    void OnMouseDown() {
        UnitSelection.SetSelected(((new Vector2(transform.position.x, transform.position.z)), this));
    }

    void OnMouseEnter() {
        tile = CreateGrid.GetTile((new Vector2(transform.position.x, transform.position.z)), tile);
        tile.EnableHighlight();
    }

    void OnMouseExit() {
        tile = CreateGrid.GetTile((new Vector2(transform.position.x, transform.position.z)), tile);
        tile.DisableHighlight();
    }

    void InitStats() {
        currHealth = unitStats.Health;
        currSpeed = unitStats.Speed;
        currStrength = unitStats.Strength;
        currMagic = unitStats.Magic;
        currRange = unitStats.Range;
        currDefense = unitStats.Defense;
        currResilience = unitStats.Resilience;
        currMovement = unitStats.Movement;
    }

    public void MoveUnit(int dis) {
        currMovement -= dis;
    }

    public int GetMovement() {
        return currMovement;
    }

    public int GetHealth() {
        return currHealth;
    }

    public int GetDefense() {
        return currDefense;
    }

    // amount is the raw damage,
    // isMagic asks if the damage is from a magic or physical weapon,
    // enemy is the Unit that is being damaged
    public void Damage(int amount, bool isMagic) {
        int actualDamage = (amount - (isMagic ? currResilience : currDefense));
        
        currHealth -= actualDamage;

        if (currHealth <= 0) {
            Death();
        }
    }

    void Death() {
        Vector3 trans = transform.position;
        Vector2 pos = new Vector2(trans.x, trans.z);
        CreateGrid.GetTile(pos, tile).SetAttribute(Tile.Attribute.Normal);
        Destroy(this.gameObject);
    }


    private void OnCollisionEnter(Collision collided)
    {
        switch (collided.gameObject.name)
        {
            case "potion_blue_fab_small":
                currMagic += 5;
                Destroy(collided.gameObject);
                break;
            case "potion_red_fab_small":
                currStrength += 5;
                Destroy(collided.gameObject);
                break;
            case "potion_green_fab_small":
                if (currHealth <= unitStats.Health - 5)
                    currHealth += 5;
                else
                    currHealth += (unitStats.Health - currHealth);

                currDefense += 5;
                Destroy(collided.gameObject);
                break;
            case "potion_yellow_fab_small":
                currSpeed += 1;
                currMovement += 1;
                Destroy(collided.gameObject);
                break;
        }
        
    }
}
