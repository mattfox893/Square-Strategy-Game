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
            lastTile.SetAttribute(Attribute.Normal);
        }
        
        tile = GridManager.GetTile(GridManager.to2D(transform), tile);
        tile.SetAttribute(Attribute.Impassable);
        lastTile = tile;
    }
}
