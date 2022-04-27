using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileAttribute : MonoBehaviour
{
    Tile tile;
    [SerializeField] Attribute attribute;

    void Update()
    {
        tile = GridManager.GetTile(GridManager.to2D(transform), tile);
        tile.SetAttribute(attribute);
    }
}
