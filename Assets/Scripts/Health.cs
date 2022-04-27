using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public float health;
    public float maxHealth;

    public GameObject healthUI;
    public Slider healthSlider;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        healthSlider.value = SliderHealth();
    }

    // Update is called once per frame
    void Update()
    {
        healthSlider.value = SliderHealth();
        if (health <= 0)
        {
            Destroy(gameObject);
        }

        health = Mathf.Min(health, maxHealth);
    }

    float SliderHealth()
    {
        return health / maxHealth;
    }
}
