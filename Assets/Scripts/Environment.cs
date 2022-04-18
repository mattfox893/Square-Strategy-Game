using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Environment : MonoBehaviour
{
    Tile tile;
    Tile.Attribute attr;

    void Update() {
        if (tile == null) {
            attr = Tile.Attribute.Impassable;
            tile = CreateGrid.getTile(transform, tile);
            tile.setAttribute(attr);
        }
    }
}
