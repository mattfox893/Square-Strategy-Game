using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public Inventory inventory;
    Unit selected;
    List<Item> items;
    public GameObject itemPrefab;
    public GameObject itemContainer;
    // Start is called before the first frame update
    void Start()
    {
        inventory.gameObject.SetActive(false);
    }
    public void ToggleInventory()
    {
        if (!gameObject.activeInHierarchy)
        {
            gameObject.SetActive(true);
            loadItems();
        } 
        else
        {
            closeInventory();
        }
        
    }

    public void closeInventory()
    {
        gameObject.SetActive(false);
        foreach (Transform child in itemContainer.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }

    public void loadItems()
    {
        selected = UnitSelection.Instance.GetSelected();
        inventory = selected.GetInventory();
        items = inventory.GetItems();
        Debug.Log(items.Count);
        for (int i = 0; i < items.Count; i++)
        {
            GameObject newItem = Instantiate(itemPrefab, itemContainer.transform);
            Vector3 pos = newItem.transform.position;
            pos.y = pos.y - 40*i;
            newItem.transform.position = pos;
            newItem.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = items[i].name;

        }

    }

    // Update is called once per frame
    void Update()
    {
        selected = UnitSelection.Instance.GetSelected();
    }
}
