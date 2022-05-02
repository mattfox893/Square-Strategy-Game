using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public TurnState turn;
    public static TurnManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        ChangeState(TurnState.PlayerTurn);
    }

    public void ChangeState(TurnState newState)
    {
        turn = newState;

        switch (newState)
        {
            case TurnState.PlayerTurn:
                // logic for the start of player turn
                UnitSelection.Instance.Deselect();
                //UnitManager.Instance.EndAll(Team.Enemy);
                UnitManager.Instance.StartAll(Team.Ally);
                break;
            case TurnState.EnemyTurn:
                // logic for the start of enemy turn
                UnitSelection.Instance.Deselect();

                //.Instance.EndAll(Team.Ally);
                UnitManager.Instance.StartAll(Team.Enemy);
                UnitManager.Instance.EnemyTurn();
                UnitManager.Instance.CheckTeamStatus(Team.Enemy);
                break;
            default:
                Debug.Log($"ERROR! TurnManager.ChangeState() was called with an invalid argument: {newState}");
                break;
        }
    }
}

public enum TurnState
{
    PlayerTurn = 0,
    EnemyTurn = 1,
}
