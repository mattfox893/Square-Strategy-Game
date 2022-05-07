using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionUI : MonoBehaviour
{
    public Button attackButton;
    public Button itemButton;
    public Button endTurnButton;
    public InventoryUI inventory;
    Unit selected;
    Color startCol;

    void Start()
    {
        attackButton.onClick.AddListener(delegate { Action(0); });
        startCol = attackButton.GetComponent<Image>().color;
        itemButton.onClick.AddListener(delegate { Action(1); });
        endTurnButton.onClick.AddListener(delegate { Action(2); });
    }

    private void Awake()
    {

    }

    // For now Action only removes the bar. Other actions can be added in later
    private void Action(int action)
    {
        selected = UnitSelection.Instance.GetSelected();

        // 0 - Attack
        // 1 - Item
        // 2 - End Turn
        switch (action)
        {
            case 0: // Attack
                UnitSelection.Instance.selected.state = UnitState.ActReady;
                inventory.closeInventory();
                break;
            case 1: // Item/Open inv
                inventory.ToggleInventory();
                break;
            case 2: // Pass/End turn for unit
                inventory.closeInventory();
                selected.EndTurn();
                break;
            default:
                Debug.Log($"ERROR! Invalid action: {action}");
                break;
        }

    }

    public void Update()
    {
        if (UnitSelection.Instance.selected.state == UnitState.ActReady)
        {
            attackButton.GetComponent<Image>().color = Color.red;
        } else
        {
            attackButton.GetComponent<Image>().color = startCol;
        }
    }
}
