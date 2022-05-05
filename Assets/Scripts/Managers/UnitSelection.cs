using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSelection : MonoBehaviour
{
    public Unit selected;
    public Tile selectedTile;
    public GameObject highlight;
    public static UnitSelection Instance;

    private void Awake()
    {
        Instance = this;    
    }

    void Start() 
    {
        Deselect();
    }

    private void Update()
    {
        if (highlight != null && selected != null)
        {
            highlight.transform.position = selected.transform.position;
        }
        
    }

    public void SetSelected(Unit toSelect)
    {
        // invalid input
        if (toSelect == null)
            return;

        // trying to select a friendly unit that has already acted
        if (toSelect.team == Team.Ally && toSelect.state == UnitState.Acted)
            return;

        if (selected != null && toSelect.team == Team.Ally && selected.state == UnitState.ActReady)
            selected.state = UnitState.NotActed;

        selectedTile = GridManager.GetTile(toSelect.GetGridPos(), selectedTile);

        // if the unit is not on a valid tile
        if (selectedTile == null)
            return;

        // if the Unit selected is of Team Ally,
        if (toSelect.team == Team.Ally) 
        {
            // on selection, do this
            selectedTile.SetAttribute(Attribute.Normal);
            selected = toSelect;
            highlight.SetActive(true);
            highlight.transform.position += new Vector3(0, 1, 0);
        }

        // if the Unit selected is of Team Enemy,
        if (toSelect.team == Team.Enemy)
        {
            // if the previously selected Unit was of team Ally and waiting to select target,
            if (selected != null && selected.team == Team.Ally && selected.state == UnitState.ActReady)
            {
                if (selected.InRange(toSelect))
                {
                    selected.Attack(toSelect);
                    selected.EndTurn();
                }
            }

        }

        
        
    }

    public void Deselect()
    {
        selected = null;
        selectedTile = null;
        highlight.SetActive(false);
    }

    public Unit GetSelected() 
    {
        return selected;
    }
}
