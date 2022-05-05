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
    public void openInventory()
    {
        inventory.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        selected = UnitSelection.Instance.GetSelected();
    }
}
