using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Machine : MonoBehaviour 
{
    [Header("General info")]
    public string m_MachineName;
    public float m_MaxMachineHealth;
    public float m_MachineEnergy;
    public float m_MaxEnergyConsumption;
    public float m_Speed;
    private float m_FinalSpeed;
    public string m_ResourceType;
    [SerializeField]private Rigidbody m_Rigid;
    public GameObject m_Object;
    public Sprite m_ObjectSprite;
    public GameObject m_ObjectBluePrint;
    private Animator m_Anim;

    [Header("Extra info")]
    public float m_EnergyConsumption;
    public float m_MachineHealth;
    public float m_CurrentSpeed;
    public float m_Timer;
    public float m_AnimSpeed;

    public Resources m_Resource; // = new Resources();
    public Inventory m_InventoryScript;

	void Start () 
    {
        m_FinalSpeed = 60 / m_Speed;
        m_MachineHealth = m_MaxMachineHealth;
        m_InventoryScript = GameObject.Find("Player").GetComponent<Inventory>();
        m_Anim = GetComponent<Animator>();
        m_Rigid = GetComponent<Rigidbody>();
	}
	
	void Update () 
    {
        if (m_MachineEnergy > 0)
        {
            m_Anim.SetBool("Active", true);
            if (m_MachineEnergy >= m_MaxEnergyConsumption)
            {
                m_MachineEnergy -= m_MaxEnergyConsumption * Time.deltaTime;
                m_EnergyConsumption = m_MaxEnergyConsumption;
            }
            else
            {
                m_EnergyConsumption = m_MachineEnergy / m_MaxEnergyConsumption;
                m_MachineEnergy = 0;
            }
            m_CurrentSpeed = m_EnergyConsumption / m_MaxEnergyConsumption;
            m_Timer += m_CurrentSpeed / 1 * Time.deltaTime;
            if (m_Timer >= m_FinalSpeed)
            {
                m_Resource.m_Amount -= 1;
                m_InventoryScript.m_StoneAmount += 1;
                m_Timer = 0;
            }
        }
        else
        {
            m_Anim.SetBool("Active", false);
        }
        if (m_Resource.m_Amount <= 0)
        {
            m_Rigid.isKinematic = false;
        }
        else
        {
            m_Rigid.isKinematic = true;
        }

        if (m_MachineEnergy < 100)
        {
            m_MachineEnergy += 10 * Time.deltaTime;
        }
	}

    public void AddResourse(Resources resource)
    {
        m_Resource = resource;
    }
}
