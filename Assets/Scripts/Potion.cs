using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : Interactable, Immovables 
{
    Tile tile;

    void Start()
    {
        tile = GridManager.GetTile((new Vector2(transform.position.x, transform.position.z)), tile);
    }

    void OnMouseEnter()
    {
        tile = GridManager.GetTile((new Vector2(transform.position.x, transform.position.z)), tile);
        tile.EnableHighlight();
    }

    void OnMouseExit()
    {
        tile = GridManager.GetTile((new Vector2(transform.position.x, transform.position.z)), tile);
        tile.DisableHighlight();
    }

    /*private void OnCollisionEnter(Collision collided)
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

    }*/

}