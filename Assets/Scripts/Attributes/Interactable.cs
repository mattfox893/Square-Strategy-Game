using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public Item item;
    private void OnCollisionEnter(Collision col)
    {
        Unit collided = col.gameObject.GetComponent<Unit>();
        if (collided != null)
        {
            if (collided.team == Team.Ally)
            {
                collided.GetInventory().AddItem(item);
            }
        }
    }
}
