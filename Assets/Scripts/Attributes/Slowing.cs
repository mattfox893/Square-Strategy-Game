using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slowing : MonoBehaviour
{
    Tile tile;
    Tile lastTile;

    void Update()
    {
        if (lastTile != null)
        {
            lastTile.SetAttribute(Attribute.Normal);
        }

        tile = GridManager.GetTile(GridManager.to2D(transform), tile);
        tile.SetAttribute(Attribute.Slow);
        lastTile = tile;
    }
}
