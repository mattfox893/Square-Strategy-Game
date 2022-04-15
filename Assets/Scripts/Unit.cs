using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    Tile tile;
    public PlayerMovement moveScript;

    void Start() {
        moveScript = this.GetComponent<PlayerMovement>();
        tile = getTile();
    }

    void OnMouseDown() {
        UnitSelection.setSelected(((new Vector2(transform.position.x, transform.position.z)), this));
    }

    void OnMouseEnter() {
        tile = getTile();
        tile.enableHighlight();
    }

    void OnMouseExit() {
        tile = getTile();
        tile.disableHighlight();
    }

    Tile getTile() {
        GameObject finding = GameObject.Find($"Tile {transform.position.x} {transform.position.z}");
        return finding == null ? tile : finding.GetComponent<Tile>();
    }
}
