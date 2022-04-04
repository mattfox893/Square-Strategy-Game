using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateGrid : MonoBehaviour
{
    public int width, height;
    public Tile tilePrefab;


    void Start()
    {
        Generate();
    }

    void Generate() {
        for (int x = 0; x < width; x++) {
            for (int z = 0; z < height; z++) {
                var newTile = Instantiate(tilePrefab, new Vector3(x, 0, z), Quaternion.identity);
                newTile.name = $"Tile {x} {z}";
                newTile.transform.parent = this.gameObject.transform;
            }
        }
    }
}
