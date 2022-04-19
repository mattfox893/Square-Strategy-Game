using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Impassable : MonoBehaviour
{
    Tile tile;
    Tile lastTile;

    void Update()
    {
        if(lastTile != null) {
            lastTile.setAttribute(Tile.Attribute.Normal);
        }
        
        tile = CreateGrid.getTile((new Vector2(transform.position.x, transform.position.z)), tile);
        tile.setAttribute(Tile.Attribute.Impassable);
        lastTile = tile;
    }
}
