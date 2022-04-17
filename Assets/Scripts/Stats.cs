using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Stat Values")]
public class Stats : ScriptableObject
{


    public int Health;
    public int currHealth;
    public int Speed;
    public int currSpeed;
    public int Strength;
    public int currStrength;
    public int Magic;
    public int currMagic;
    public int Range;
    public int currRange;
    public int Defense;
    public int currDefense;

    private void OnEnable()
    {
        Health = currHealth;
        Speed = currSpeed;
        Strength = currStrength;
        Magic = currMagic;
        Range = currRange;
        Defense = currDefense;
    }
}
