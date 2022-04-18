using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public GameObject highlight;
    Vector2 position;
    Unit onTile;
    public enum Attribute {
        Normal,
        Impassable,
        Slow
    }
    Attribute attr;

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

    public void setAttribute(Attribute a) {
        attr = a;
    }
}
