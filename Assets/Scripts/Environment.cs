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
            tile = CreateGrid.getTile((new Vector2(transform.position.x, transform.position.z)), tile);
            tile.setAttribute(attr);
        }
    }
}
