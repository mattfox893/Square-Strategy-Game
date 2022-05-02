using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    bool isMoving;
    Unit currUnit;
    Vector3 direction, target, start;
    Tile startTile;
    float journeyDist, timeElapsed;

    [SerializeField] float moveSpeed = 4f;
    public static EnemyBehavior Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (isMoving && currUnit != null)
        {
            timeElapsed += Time.deltaTime;
            Move(currUnit, DetermineClosest(currUnit));
        }
    }

    public void Act(Unit unit)
    {
        if (!AttackEnemyWithinRange(unit))
        {
            for (int i = unit.GetMovement(); i > 0; i--)
            {
                if (!isMoving && CheckMoveValid(unit, DetermineClosest(unit)))
                {
                    //StartCoroutine(Move(unit, DetermineClosest(unit)));
                    SetMoving(unit, DetermineClosest(unit));
                    currUnit = unit;
                    AttackEnemyWithinRange(unit);
                }
            }
        }

        //Move(unit, "up");
        //Move(unit, "left");

        unit.state = UnitState.Acted;
    }

    void SetMoving(Unit unit, string dir)
    {
        direction = ReturnStringAsDirection(dir);
        int moveCost = 1;
        target = unit.transform.position + direction;
        start = unit.transform.position;
        startTile = GridManager.GetTile(new Vector2(start.x, start.z), null);
        journeyDist = 0.1f * moveSpeed;

        if (startTile.GetAttribute() == Attribute.Slow)
        {
            moveCost = 2;
        }

        unit.MoveUnit(moveCost);

        isMoving = true;
    }

    // find the closest unit, return a string of their relative direction
    string DetermineClosest(Unit unit)
    {
        Vector2 unitPos = unit.GetGridPos(), targetPos;
        string dir = "";

        foreach (Unit target in UnitManager.Instance.units)
        {
            if (target.team == Team.Ally)
            {
                targetPos = target.GetGridPos();

                if (targetPos.x > unitPos.x && CheckMoveValid(unit, "right"))
                {
                    dir = "right";
                }
                else if (targetPos.x < unitPos.x && CheckMoveValid(unit, "left"))
                {
                    dir = "left";
                }
                else if (targetPos.y > unitPos.y && CheckMoveValid(unit, "up"))
                {
                    dir = "up";
                }
                else if (targetPos.y < unitPos.y && CheckMoveValid(unit, "down"))
                {
                    dir = "down";
                }
            }
        }

        return dir;
    }

    // try to attack an enemy within range of unit, return true if successful and false otherwise
    bool AttackEnemyWithinRange(Unit unit)
    {
        foreach (Unit target in UnitManager.Instance.units)
        {
            // if there is an "Ally" within range to attack,
            if (target.team == Team.Ally && unit.inRange(target))
            {
                // attack the target
                unit.Attack(target);
                return true;
            }
        }
        return false;
    }

    // if moving in dir would clash with an immovable tile,
    bool CheckMoveValid(Unit unit, string dir)
    {
        Vector3 direction = ReturnStringAsDirection(dir);
        Vector3 target = unit.transform.position + direction;
        Tile targetTile = GridManager.GetTile(new Vector2(target.x, target.z), null);

        return (targetTile != null && targetTile.GetAttribute() != Attribute.Impassable);
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

    void Move(Unit unit, string dir)
    {
        /*Vector3 direction = ReturnStringAsDirection(dir);
        int moveCost = 0;
        Vector3 target = unit.transform.position + direction;
        Vector3 start = unit.transform.position;
        Tile startTile = GridManager.GetTile(new Vector2(start.x, start.z), null);
        Tile targetTile = GridManager.GetTile(new Vector2(target.x, target.z), null);*/

        if (timeElapsed <= journeyDist)
        {
            // unit.transform.position += (target - start) * moveSpeed * Time.deltaTime;
            unit.transform.position = Vector3.Lerp(start, target, timeElapsed / journeyDist);
        }

        if (Vector3.Distance(target, unit.transform.position) <= 0.1f)
        {
            unit.transform.position = new Vector3(Mathf.Round(unit.transform.position.x), unit.transform.position.y, Mathf.Round(unit.transform.position.z));
            isMoving = false;
            currUnit = null;
            timeElapsed = 0f;
        }
        //unit.transform.position = new Vector3(Mathf.Round(unit.transform.position.x), unit.transform.position.y, Mathf.Round(unit.transform.position.z));
    }

/*
    IEnumerator Move(Unit unit, string dir)
    {
        isMoving = true;
        Vector3 direction = ReturnStringAsDirection(dir);
        int moveCost = 0;
        Vector3 target = unit.transform.position + direction;
        Vector3 start = unit.transform.position;
        Tile startTile = GridManager.GetTile(new Vector2(start.x, start.z), null);
        Tile targetTile = GridManager.GetTile(new Vector2(target.x, target.z), null);

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

            unit.MoveUnit(moveCost);

            while (Vector3.Distance(start, unit.transform.position) <= 1f)
            {
                unit.transform.position += (target - start) * moveSpeed * Time.deltaTime;
                yield return null;
            }
        }

        unit.transform.position = new Vector3(Mathf.Round(unit.transform.position.x), unit.transform.position.y, Mathf.Round(unit.transform.position.z));
        isMoving = false;
    }
*/

    IEnumerator Wait(float t)
    {
        Debug.Log("waiting...");
        yield return new WaitForSeconds(t);
        Debug.Log("done waiting.");
    }
}
