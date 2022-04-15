using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public GameObject highlight;
    Vector2 position;
    Unit onTile;

    void Start() {
        position = new Vector2(transform.position.x, transform.position.z);
    }

    void OnMouseEnter() {
        enableHighlight();
    }

    void OnMouseExit() {
        disableHighlight();
    }

    
    public void enableHighlight() {
        highlight.SetActive(true);
    }

    public void disableHighlight() {
        highlight.SetActive(false);
    }

}
