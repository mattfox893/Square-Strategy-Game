using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public TurnState turn;

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
