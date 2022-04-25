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
            lastTile.SetAttribute(Tile.Attribute.Normal);
        }
        
        tile = CreateGrid.GetTile((new Vector2(transform.position.x, transform.position.z)), tile);
        tile.SetAttribute(Tile.Attribute.Impassable);
        lastTile = tile;
    }
}
