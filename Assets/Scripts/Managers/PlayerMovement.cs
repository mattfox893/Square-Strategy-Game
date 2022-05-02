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
        if (UnitSelection.Instance.GetSelected() != null)
        {
            // set the local selected to the static selected Unit
            selected = UnitSelection.Instance.GetSelected();

            // if the selected Unit is of team Ally and it is the player's turn,
            if (selected.team == Team.Ally && TurnManager.Instance.turn == TurnState.PlayerTurn)
            {
                // if we aren't already moving, the selected Unit has movement left, and it hasn't ended its own turn,
                if (!isMoving && selected.GetMovement() > 0 && selected.state == UnitState.NotActed)
                {
                    if (Input.GetKeyDown(KeyCode.W))
                    {
                        StartCoroutine(Move("up"));
                    }
                    else if (Input.GetKeyDown(KeyCode.A))
                    {
                        StartCoroutine(Move("left"));
                    }
                    else if (Input.GetKeyDown(KeyCode.S))
                    {
                        StartCoroutine(Move("down"));
                    }
                    else if (Input.GetKeyDown(KeyCode.D))
                    {
                        StartCoroutine(Move("right"));
                    }
                }
            }
        }

    }

    // convert a given string into a vector3 direction
    Vector3 ReturnStringAsDirection(string dir)
    {
        Vector3 direction;

        switch (dir)
        {
            case "left":
                direction = new Vector3(-1, 0, 0);
                break;
            case "down":
                direction = new Vector3(0, 0, -1);
                break;
            case "right":
                direction = new Vector3(1, 0, 0);
                break;
            case "up":
                direction = new Vector3(0, 0, 1);
                break;
            default:
                Debug.Log($"ERROR! Incorrect Movement Call: {dir}");
                direction = new Vector3(0, 0, 0);
                break;
        }

        return direction;
    }

    Vector3 DirectionAsRotation(Vector3 dir)
    {
        if (dir == new Vector3(-1, 0, 0))
        {
            return new Vector3(0, -90, 0);
        }
        else if (dir == new Vector3(0, 0, -1))
        {
            return new Vector3(0, 180, 0);
        }
        else if (dir == new Vector3(1, 0, 0))
        {
            return new Vector3(0, 90, 0);
        }
        else if (dir == new Vector3(0, 0, 1))
        {
            return new Vector3(0, 0, 0);
        }
        else
        {
            return new Vector3(0, 0, 0);
        }
    }

    IEnumerator Move(string dir)
    {
        isMoving = true;
        Vector3 direction = ReturnStringAsDirection(dir);
        int moveCost = 0;
        target = selected.transform.position + direction;
        start = selected.transform.position;
        startTile = GridManager.GetTile(new Vector2(start.x, start.z), startTile);
        targetTile = GridManager.GetTile(new Vector2(target.x, target.z), null);

        selected.animator.SetBool("isWalking", true);
        if (targetTile != null && targetTile.GetAttribute() != Attribute.Impassable)
        {
            if (startTile.GetAttribute() == Attribute.Slow)
            {
                moveCost = 2;
            }
            else
            {
                moveCost = 1;
            }

            selected.MoveUnit(moveCost);
            selected.transform.localEulerAngles = DirectionAsRotation(direction);

            while (Vector3.Distance(start, selected.transform.position) <= 1f)
            {
                selected.transform.position += (target - start) * moveSpeed * Time.deltaTime;
                yield return null;
            }

        }

        selected.transform.position = 
            new Vector3(Mathf.Round(selected.transform.position.x), selected.transform.position.y, Mathf.Round(selected.transform.position.z));

        isMoving = false;
        selected.animator.SetBool("isWalking", false);
    }
}
