using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    bool isMoving;
    
    [SerializeField] float moveSpeed = 4f;
    public static EnemyBehavior Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void Act(Unit unit)
    {
        // if no units in attack range, move towards closest unit
        /*foreach (Unit target in UnitManager.Instance.units)
        {
            if (unit.inRange(target))
            {

            }

            Move(unit, "up");
        }*/
        StartCoroutine(Move(unit, "up"));
        unit.state = UnitState.Acted;
    }

    // find the closest unit
    Unit DetermineClosest(Unit unit)
    {
        foreach (Unit target in UnitManager.Instance.units)
        {

        }

        return null;
    }

    IEnumerator Move(Unit unit, string dir)
    {            
        isMoving = true;
        Vector3 direction = new Vector3(0, 0, 0);
        int moveCost = 0;
        Vector3 target;
        Vector3 start;
        Tile startTile;
        Tile targetTile;

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
                break;
        }

        target = unit.transform.position + direction;
        start = unit.transform.position;
        startTile = GridManager.GetTile(new Vector2(start.x, start.z), null);
        targetTile = GridManager.GetTile(new Vector2(target.x, target.z), null);

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
}
