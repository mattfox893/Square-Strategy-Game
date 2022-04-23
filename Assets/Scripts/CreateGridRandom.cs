using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateGridRandom : MonoBehaviour
{
    public int width, length;
    public Tile tilePrefab;
    public GameObject potion1, potion2, potion3, potion4;



    void Start()
    {
        Generate();
    }

    void Generate() {
        for (int x = 0; x < width; x++) {
            for (int z = 0; z < length; z++) {
                Vector3 newTileLoc = new Vector3(x, 0, z);
                var newTile = Instantiate(tilePrefab, newTileLoc, Quaternion.identity);
                newTile.name = $"Tile {x} {z}";
                newTile.transform.parent = this.gameObject.transform;
                var immovable = new GameObject("Immovable"); ;
                int rand = Random.Range(0, width * length);
                if (rand == 0)
                {
                    immovable = Instantiate(potion1, new Vector3(x, 1, z), Quaternion.identity);
                }
                else if (rand == 1)
                {
                    immovable = Instantiate(potion2, new Vector3(x, 1, z), Quaternion.identity);
                }
                else if (rand == 2)
                {
                    immovable = Instantiate(potion3, new Vector3(x, 1, z), Quaternion.identity);
                }
                else if (rand == 3)
                {
                    immovable = Instantiate(potion4, new Vector3(x, 1, z), Quaternion.identity);
                }
                immovable.transform.parent = this.gameObject.transform;
            }
        }
    }

    public static Tile getTile(Vector2 pos, Tile last) {
        GameObject finding = GameObject.Find($"Tile {pos.x} {pos.y}");
        return finding == null ? last : finding.GetComponent<Tile>();
    }
}
