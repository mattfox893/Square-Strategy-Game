using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionUI : MonoBehaviour
{
    public GameObject button;//Its your button
                          // Use this for initialization
    void Start()
    {
        button.SetActive(false);
    }

    // Update is called once per frame
    private void OnMouseDown()
    {

        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Pressed left click.");
            button.SetActive(true);

        }


    }

    public void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            button.SetActive(false);
        }

    }
}
