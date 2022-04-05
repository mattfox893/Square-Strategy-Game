using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public GameObject highlight;

    void OnMouseEnter() {
        highlight.SetActive(true);
    }

    void OnMouseExit() {
        
        highlight.SetActive(false);
    }
}
