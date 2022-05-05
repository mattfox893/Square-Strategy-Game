using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : Interactable
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


}