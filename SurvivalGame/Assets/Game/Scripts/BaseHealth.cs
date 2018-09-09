using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseHealth : MonoBehaviour 
{
    public float m_MaxHealth;
    public float m_CurrentHealth;
    public float m_BaseHealthRegen;
    public Text m_HealthText;
	
    void Start()
    {
        m_CurrentHealth = m_MaxHealth;
    }

	void Update () 
    {
        m_HealthText.text = m_CurrentHealth.ToString("F0") + "/" + m_MaxHealth.ToString("F0");
        if (m_CurrentHealth < m_MaxHealth)
        {
            m_CurrentHealth += m_BaseHealthRegen * Time.deltaTime;
        }
        if (m_CurrentHealth > m_MaxHealth)
        {
            m_CurrentHealth = m_MaxHealth;
        }
	}
}
