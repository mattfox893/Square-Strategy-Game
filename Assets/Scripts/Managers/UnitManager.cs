using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    public List<Unit> units;
    public static UnitManager Instance;

    private void Awake()
    {
        Instance = this;
        
    }

    private void Start()
    {
        units = Resources.LoadAll<Unit>("Units").ToList();
    }

    // Called at the start of a turn for Team team
    public void ResetUnits(Team team)
    {
        foreach(Unit unit in units)
        {
            if (unit.team == team)
            {
                unit.TurnReset();
            }
        }
    }
}
