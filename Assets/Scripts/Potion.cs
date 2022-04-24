using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : MonoBehaviour, Immovables
{
    Tile tile;

    void Start()
    {
        tile = CreateGrid.getTile((new Vector2(transform.position.x, transform.position.z)), tile);
    }

    void OnMouseEnter()
    {
        tile = CreateGrid.getTile((new Vector2(transform.position.x, transform.position.z)), tile);
        tile.enableHighlight();
    }

    void OnMouseExit()
    {
        tile = CreateGrid.getTile((new Vector2(transform.position.x, transform.position.z)), tile);
        tile.disableHighlight();
    }

}