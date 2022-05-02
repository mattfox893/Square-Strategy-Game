using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    public List<Unit> units, allies, enemies;

    public static UnitManager Instance;

    private void Awake()
    {
        Instance = this;
        
    }

    private void Start()
    {
        units = FindObjectsOfType<Unit>().ToList();
        foreach (Unit unit in units)
        {
            if (unit.team == Team.Ally)
            {
                allies.Add(unit);
            } else if (unit.team == Team.Enemy)
            {
                enemies.Add(unit);
            }
        }
    }

    // Check if all members of team have acted
    public void CheckTeamStatus(Team team)
    {
        foreach (Unit unit in units)
        {
            // if any Unit on team hasn't acted yet, exit this function.
            if (unit.team == team && unit.state == UnitState.NotActed)
            {
                return;
            }
        }
        // if the current state is the player's turn, change to enemy's turn, else vice versa.
        TurnState newState = TurnManager.Instance.turn == TurnState.PlayerTurn ? TurnState.EnemyTurn : TurnState.PlayerTurn;

        TurnManager.Instance.ChangeState(newState);
    }

    // Called at the start of a turn for Team team
    public void StartAll(Team team)
    {
        foreach(Unit unit in units)
        {
            if (unit.team == team)
            {
                unit.StartTurn();
            }
        }
    }

    // Called at the end of a turn for Team team
    public void EndAll(Team team)
    {
        foreach (Unit unit in units)
        {
            if (unit.team == team)
            {
                unit.EndTurn();
            }
        }
    }

    public void EnemyTurn()
    {
        foreach (Unit unit in enemies)
        {
            EnemyBehavior.Instance.Act(unit);
        }
    }
}
