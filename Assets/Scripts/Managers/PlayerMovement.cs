using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 4f;
    bool isMoving;
    Vector3 target;
    Vector3 start;
    Tile startTile;
    Tile targetTile;
    Unit selected;
    //Animator animator;
    // Update is called once per frame

    void Start()
    {
        //animator = GetComponent<Animator>();
    }
    void Update()
    {
        // if a Unit has been selected,
        if (UnitSelection.GetSelected() != null)
        {
            // set the local selected to the static selected Unit
            selected = UnitSelection.GetSelected();

            // if the selected Unit is of team Ally and it is the player's turn,
            if (selected.team == Team.Ally && TurnManager.Instance.turn == TurnState.PlayerTurn)
            {
                if (isMoving && targetTile.GetAttribute() != Attribute.Impassable)
                {
                    //animator.SetBool("isWalking", true);
                    Moving();
                }
                else if (isMoving)
                {
                    isMoving = false;
                    //animator.SetBool("isWalking", false);
                }

                if (!isMoving && selected.GetMovement() > 0 && selected.state == UnitState.NotActed)
                {
                    if (Input.GetKeyDown(KeyCode.W))
                    {
                        Move("up");
                    }
                    else if (Input.GetKeyDown(KeyCode.A))
                    {
                        Move("left");
                    }
                    else if (Input.GetKeyDown(KeyCode.S))
                    {
                        Move("down");
                    }
                    else if (Input.GetKeyDown(KeyCode.D))
                    {
                        Move("right");
                    }
                }
            }
        }
    }

    void Moving() {
        if (Vector3.Distance(start, selected.transform.position) > 1f)
        {
            isMoving = false;
            return;
        }
        selected.transform.position += (target - start) * moveSpeed * Time.deltaTime;
        return;
    }

    void Move(string dir) {
        Vector3 direction = new Vector3(0,0,0);
        int moveCost = 0;

        switch (dir) 
        {
            case "left":
                direction = new Vector3(-1,0,0);
                break;
            case "down":
                direction = new Vector3(0,0,-1);
                break;
            case "right":
                direction = new Vector3(1,0,0);
                break;
            case "up":
                direction = new Vector3(0,0,1);
                break;
            default:
                Debug.Log($"ERROR! Incorrect Movement Call: {dir}");
                break;
        }

        target = selected.transform.position + direction;
        start = selected.transform.position;
        startTile = GridManager.GetTile(new Vector2(start.x, start.z), startTile);
        targetTile = GridManager.GetTile(new Vector2(target.x, target.z), startTile);

        if (targetTile.GetAttribute() != Attribute.Impassable)
        {
            if (startTile.GetAttribute() == Attribute.Slow)
            {
                moveCost = 2;
            }
            else
            {
                moveCost = 1;
            }
        }

        selected.MoveUnit(moveCost);

        isMoving = true;
    }
}
