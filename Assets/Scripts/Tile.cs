using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public GameObject highlight;
    Vector2 position;
    Unit onTile;
    Immovables immovable;
    public enum Attribute {
        Normal,
        Impassable,
        Slow
    }
    Attribute attr;

    void Start() {
        attr = Attribute.Normal;
        position = new Vector2(transform.position.x, transform.position.z);
    }

    void OnMouseEnter() {
        EnableHighlight();
    }

    void OnMouseExit() {
        DisableHighlight();
    }
    
    public void EnableHighlight() {
        highlight.SetActive(true);
    }

    public void DisableHighlight() {
        highlight.SetActive(false);
    }

    public void SetAttribute(Attribute a) {
        attr = a;
    }

    public Attribute GetAttribute() {
        return attr;
    }

    public Unit GetUnit() {
        return onTile;
    }
}
