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
        
    }
    
    // Update is called once per frame
    void Update()
    {
        if (TurnManager.Instance.turn == TurnState.PlayerTurn)
        {
            endTurn.onClick.AddListener(delegate { TurnManager.Instance.ChangeState(TurnState.EnemyTurn); });
        }
        
    }
}
