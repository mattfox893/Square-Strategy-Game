using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndTurn : MonoBehaviour
{
    public Button endTurn;

    // Start is called before the first frame update
    void Start()
    {
        Button btn = endTurn.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
        //endTurn.onClick.AddListener(delegate { TurnManager.Instance.ChangeState(TurnState.EnemyTurn); });
    }

    // Update is called once per frame
    void TaskOnClick()
    {
        if (TurnManager.Instance.turn == TurnState.PlayerTurn)
        {
            TurnManager.Instance.ChangeState(TurnState.EnemyTurn);
        }
    }
}
