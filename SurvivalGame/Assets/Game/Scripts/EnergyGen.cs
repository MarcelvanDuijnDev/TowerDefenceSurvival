using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyGen : MonoBehaviour 
{
    [SerializeField]private float m_Range;
    [SerializeField]private float m_Health;
    [SerializeField]private float m_MaxEnergyRegen;

    public float m_CurrentEnergyRegen;

    void Update()
    {
        m_CurrentEnergyRegen = m_MaxEnergyRegen;
    }

}
