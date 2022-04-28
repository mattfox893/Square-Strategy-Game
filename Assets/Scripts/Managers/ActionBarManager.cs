using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class ActionBarManager : MonoBehaviour
{
    public GameObject ActionBar;
    Unit selected;
    bool activeBar;
    Vector2 unitPos;

    // Start is called before the first frame update
    void Start()
    {
        ActionBar.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if (UnitSelection.GetSelected() != null)
        {
            // set the local selected to the static selected Unit
            selected = UnitSelection.GetSelected();

            // if the selected Unit is of team Ally and it is the player's turn,
            if (selected.team == Team.Ally && TurnManager.Instance.turn == TurnState.PlayerTurn)
            {
                if (activeBar == false)
                {
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        //Set state to cannot move
                        selected.state = UnitState.ActReady;

                        unitPos = selected.GetGridPos();
                        ActionBar.transform.position = new Vector3(unitPos.x + 1.5f, 0.6f, unitPos.y);

                        ActionBar.SetActive(true);
                        EventSystem.current.SetSelectedGameObject(ActionBar);
                        activeBar = true;
                    }
                }
                else
                {
                    if ((Input.GetMouseButtonUp(0) && !EventSystem.current.IsPointerOverGameObject()) || Input.GetKeyDown(KeyCode.Space))
                    {
                        Debug.Log("Clicked out");
                        ActionBar.SetActive(false);
                        EventSystem.current.SetSelectedGameObject(null);
                        selected.state = UnitState.NotActed;
                        activeBar = false;
                    }
                }

            }
        }

        



    }
}
