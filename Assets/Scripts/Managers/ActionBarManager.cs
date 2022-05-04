using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
public class ActionBarManager : MonoBehaviour
{
    public GameObject ActionBar;
    public GameObject OnSelectedUI;
    public TextMeshProUGUI Name;
    public TextMeshProUGUI Stats;
    Unit selected;

    // Start is called before the first frame update
    void Start()
    {
        OnSelectedUI.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if (UnitSelection.Instance.GetSelected() != null)
        {

            // set the local selected to the static selected Unit
            selected = UnitSelection.Instance.GetSelected();
            
            Name.text = selected.name;
            Stats.text =
                $"{selected.currHealth}/{selected.maxHealth}\n" +
                $"{selected.currSpeed}\n" +
                $"{selected.currStrength}\n" +
                $"{selected.currMagic}\n" +
                $"{selected.currRange}\n" +
                $"{selected.currDefense}\n" +
                $"{selected.currResilience}\n" +
                $"{selected.currMovement}/{selected.maxMovement}";


            // if the selected Unit is of team Ally and it is the player's turn,
            if (selected.team == Team.Ally && TurnManager.Instance.turn == TurnState.PlayerTurn && selected.state == UnitState.NotActed)
            {
                OnSelectedUI.SetActive(true);
            }
            

        }
        else
        {
            OnSelectedUI.SetActive(false);
        }




    }
}
