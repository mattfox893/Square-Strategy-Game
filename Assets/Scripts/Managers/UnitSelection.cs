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
            // if the previously selected Unit was of team Ally and waiting to select target,
            if (selected != null && selected.team == Team.Ally && selected.state == UnitState.ActReady)
            {
                // do something like set the valid target and change states
            }

            selected = toSelect;
        }

        highlight.SetActive(true);
        highlight.transform.position += new Vector3(0, 1, 0);
        
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
