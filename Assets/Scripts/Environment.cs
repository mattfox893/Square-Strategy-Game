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
            tile = getTile();
            tile.setAttribute(attr);
        }
    }

    Tile getTile() {
        GameObject finding = GameObject.Find($"Tile {transform.position.x} {transform.position.z}");
        return finding == null ? tile : finding.GetComponent<Tile>();
    }
}
