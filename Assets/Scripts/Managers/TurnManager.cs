using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public TurnState turn;
    public static TurnManager Instance;

    private void Start()
    {
        Instance = this;
        ChangeState(TurnState.PlayerTurn);
    }

    public void ChangeState(TurnState newState)
    {
        turn = newState;

        switch (newState)
        {
            case TurnState.PlayerTurn:
                // logic for the start of player turn
                UnitManager.Instance.ResetUnits(Team.Ally);
                break;
            case TurnState.EnemyTurn:
                // logic for the start of enemy turn
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
