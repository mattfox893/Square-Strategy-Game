using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSelection : MonoBehaviour
{
    public static (Vector2, Unit) selected;
    public static Tile selectedTile;

    void Start() 
    {
        selected = (new Vector2(0,0), null);
        selectedTile = null;
    }

    public static void SetSelected((Vector2, Unit) toSelect)
    {
        selectedTile = GridManager.GetTile(toSelect.Item1, selectedTile);
        // if the unit selected is allowed to be selected,
        if (toSelect.Item2.selectable) 
        {
            // if there was a previously selected unit, disable its move script and make it impassable
            if (selected.Item2 != null)
            {
                selected.Item2.moveScript.enabled = false;
                selectedTile.SetAttribute(Attribute.Impassable);
            }

            // while selected, starting tile attribute is normal and move script is enabled
            selectedTile.SetAttribute(Attribute.Normal);
            selected = toSelect;
            selected.Item2.moveScript.enabled = true;
        }
    }

    public static (Vector2, Unit) GetSelected() 
    {
        return selected;
    }
}
