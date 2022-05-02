using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionUI : MonoBehaviour
{
    public Button attackButton;
    public Button itemButton;
    public Button endTurnButton;
    Unit selected;

    void Start()
    {
        attackButton.onClick.AddListener(delegate { Action(0); });
        itemButton.onClick.AddListener(delegate { Action(1); });
        endTurnButton.onClick.AddListener(delegate { Action(2); });
    }

    private void Awake()
    {
        selected = UnitSelection.Instance.GetSelected();
    }

    // For now Action only removes the bar. Other actions can be added in later
    private void Action(int action)
    {
        // 0 - Attack
        // 1 - Item
        // 2 - End Turn
        switch(action)
        {
            case 0: // Attack
                UnitSelection.Instance.selected.state = UnitState.ActReady;
                break;
            case 1: // Item/Open inv
                break;
            case 2: // Pass/End turn for unit
                selected.EndTurn();
                break;
            default:
                Debug.Log($"ERROR! Invalid action: {action}");
                break;
        }

    }

    public void Update()
    {

    }
}
