using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSelection : MonoBehaviour
{
    public static Unit selected;
    public static Tile selectedTile;

    void Start() 
    {
        selected = null;
        selectedTile = null;
    }

    public static void SetSelected(Unit toSelect)
    {
        if (toSelect == null)
            return;

        selectedTile = GridManager.GetTile(toSelect.GetGridPos(), selectedTile);

        if (selectedTile == null)
            return;

        // if the Unit selected is of Team Ally,
        if (toSelect.team == Team.Ally) 
        {
            // on selection, do this
            selectedTile.SetAttribute(Attribute.Normal);
            selected = toSelect;
        }

        // if the Unit selected is of Team Enemy,
        if (toSelect.team == Team.Enemy)
        {
            selected = toSelect;
        }
    }

    public static Unit GetSelected() 
    {
        return selected;
    }
}
