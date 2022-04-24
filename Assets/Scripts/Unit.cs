using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    Tile tile;
    public PlayerMovement moveScript;
    public Stats unitStats;
    int currHealth, currSpeed, currStrength, currMagic, currRange, currDefense, currMovement;

    void Start() {
        InitStats();
        moveScript = this.GetComponent<PlayerMovement>();
        tile = CreateGrid.getTile((new Vector2(transform.position.x, transform.position.z)), tile);
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
        currMovement = unitStats.Movement;
    }

    public void moveUnit(int dis) {
        currMovement -= dis;
    }

    public int getMovement() {
        return currMovement;
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
                currHealth += 5;
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
