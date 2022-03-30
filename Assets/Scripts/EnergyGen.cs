using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyGen : MonoBehaviour 
{
    [SerializeField]private float _Range;
    [SerializeField]private float _Health;
    [SerializeField]private float _MaxEnergyRegen;

    public float _CurrentEnergyRegen;

    void Update()
    {
        _CurrentEnergyRegen = _MaxEnergyRegen;
    }
}
