using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public GameObject highlight;
    Vector2 position;
    Unit onTile;
    Attribute attr;

    void Start() {
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
        List<Unit> units = UnitManager.Instance.units;
        onTile = null;

        foreach (Unit unit in units)
        {
            if (unit.GetGridPos() == position)
            {
                onTile = unit;
            }
        }

        return onTile;
    }
}

public enum Attribute
{
    Normal = 0,
    Impassable = 1,
    Slow = 2
}
