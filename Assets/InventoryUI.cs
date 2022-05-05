using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public Inventory inventory;
    Unit selected;

    // Start is called before the first frame update
    void Start()
    {
        inventory.gameObject.SetActive(false);
    }
    public void ToggleInventory()
    {
        if (!inventory.gameObject.activeInHierarchy)
        {
            inventory.gameObject.SetActive(true);
        } 
        else
        {
            inventory.gameObject.SetActive(false);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        selected = UnitSelection.Instance.GetSelected();
    }
}
