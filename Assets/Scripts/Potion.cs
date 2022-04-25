using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : MonoBehaviour, Immovables
{
    Tile tile;

    void Start()
    {
        tile = CreateGrid.GetTile((new Vector2(transform.position.x, transform.position.z)), tile);
    }

    void OnMouseEnter()
    {
        tile = CreateGrid.GetTile((new Vector2(transform.position.x, transform.position.z)), tile);
        tile.EnableHighlight();
    }

    void OnMouseExit()
    {
        tile = CreateGrid.GetTile((new Vector2(transform.position.x, transform.position.z)), tile);
        tile.DisableHighlight();
    }

}