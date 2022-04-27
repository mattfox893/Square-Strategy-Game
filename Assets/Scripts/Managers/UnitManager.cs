using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    public UnitState state;
    public List<Unit> units;

    private void Start()
    {
        units = Resources.LoadAll<Unit>("Units").ToList();
        ChangeState(UnitState.NotActed);
    }

    public void ChangeState(UnitState newState)
    {
        state = newState;
    }
}
