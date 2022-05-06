using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public Item item;
    Unit selected;
    private void OnCollisionEnter(Collision col)
    {
        Debug.Log("collided!");
        /*Unit collided = col.gameObject.GetComponent<Unit>();
        if (collided != null)
        {
            if (collided.team == Team.Ally)
            {
                collided.GetInventory().AddItem(item);
                Destroy(this.gameObject);
            }
        }*/
    }

    private void Update()
    {
        Unit occupyingUnit = GridManager.GetTile(GridManager.to2D(transform), null).GetUnit();

        if (occupyingUnit != null)
        {
            if (occupyingUnit.team == Team.Ally)
            {
                // when an ally picks it up
                occupyingUnit.GetInventory().AddItem(item);
            } 
            else
            {
                // when an enemy picks it up
            }

            Destroy(this.gameObject);
        }
    }
}
