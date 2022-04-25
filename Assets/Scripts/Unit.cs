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
        tile = CreateGrid.getTile((new Vector2(transform.position.x, transform.position.z)), tile);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.name == "Sample Enemy") {
                    Unit enemy = GameObject.Find(hit.transform.name).GetComponent<Unit>();
                    Debug.Log($"enemy = {enemy}");
                    Unit selected = UnitSelection.getSelected().Item2;
                    if (selected != null && Vector3.Distance(enemy.transform.position, selected.transform.position) <= 1.42) {
                        damage(currStrength, false, enemy);
                    }
                }
            }
        }
    }

    void OnMouseDown() {
        UnitSelection.setSelected(((new Vector2(transform.position.x, transform.position.z)), this));
    }

    void OnMouseEnter() {
        tile = CreateGrid.getTile((new Vector2(transform.position.x, transform.position.z)), tile);
        tile.enableHighlight();
    }

    void OnMouseExit() {
        tile = CreateGrid.getTile((new Vector2(transform.position.x, transform.position.z)), tile);
        tile.disableHighlight();
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

    public void moveUnit(int dis) {
        currMovement -= dis;
    }

    public int getMovement() {
        return currMovement;
    }

    public int getHealth() {
        return currHealth;
    }

    public int getDefense() {
        return currDefense;
    }

    public void damage(int amount, bool isMagic, Unit enemy) {
        int actualDamage = (amount - (isMagic ? currResilience : currDefense));
        
        currHealth -= actualDamage;

        if (currHealth <= 0) {
            death(enemy);
        }
    }

    void death(Unit unit) {
        Vector3 trans = unit.transform.position;
        Vector2 pos = new Vector2(trans.x, trans.z);
        CreateGrid.getTile(pos, unit.tile).setAttribute(Tile.Attribute.Normal);
        Destroy(unit.gameObject);
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
