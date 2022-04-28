using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class GridManager : MonoBehaviour
{
    [SerializeField] int startWidth, startLength;
    public static int width, length;
    public Tile tilePrefab;
    public static GridManager Instance;

    void Start()
    {
        // DestroyPreviousGrid();
        width = startWidth;
        length = startLength;
        Generate();
    }

    private void DestroyPreviousGrid()
    {
        for (int x = width; x < 25; x++)
        {
            for (int z = length; z < 25; z++)
            {
                Tile currTile = GridManager.GetTile(new Vector2(x, z), null);
                if (currTile != null)
                {
                    DestroyImmediate(currTile.gameObject);
                }
            }
        }
    }

    private void Generate() 
    {
        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < length; z++)
            {
                if (GridManager.GetTile(new Vector2(x, z), null) == null)
                {
                    Vector3 newTileLoc = new Vector3(x, 0, z);
                    var newTile = Instantiate(tilePrefab, newTileLoc, Quaternion.identity);
                    newTile.name = $"Tile {x} {z}";
                    newTile.transform.parent = this.gameObject.transform;
                }
            }
        }
        /*foreach (GameObject Child in transform) {
            Child.name = $"Tile {Child.transform.position.x} {Child.transform.position.z}";
        }*/
    }

    // pos is the 2d position of the tile on the grid,
    // last is the default tile to return if Unit is not on the tile, usually the last one it was on.
    public static Tile GetTile(Vector2 pos, Tile last) 
    {
        int nearestX = (int)Math.Round(pos.x);
        int nearestY = (int)Math.Round(pos.y);
        GameObject finding = GameObject.Find($"Tile {nearestX} {nearestY}");
        return finding == null ? last : finding.GetComponent<Tile>();
    }

    public static Vector2 to2D(Transform transform)
    {
        return new Vector2(transform.position.x, transform.position.z);
    }
}

