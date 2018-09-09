using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolGetInfo : MonoBehaviour 
{
    [SerializeField]private GameObject m_ShootPos;
    [SerializeField]private Text m_NameText;
    [SerializeField]private Text m_HealthText;
    [SerializeField]private Text m_EnergyText;
    [SerializeField]private Text m_EnergyConsumptionText;
    [SerializeField]private Text m_SpeedText;

    Vector3 rot;

    Machine m_MachineScript;
    Turret m_TurretScript;
    Resources m_ResourcesScript;
	
	void Update () 
    {
        rot.x = m_ShootPos.transform.rotation.x;
        rot.y = m_ShootPos.transform.rotation.y;
        rot.z = m_ShootPos.transform.rotation.z;
        rot = Vector3.forward;

        RaycastHit hit;

        if (Physics.Raycast(m_ShootPos.transform.position, m_ShootPos.transform.TransformDirection(rot), out hit, 20))
        {
            if (hit.transform.gameObject.tag == "Machine")
            {
                if (m_MachineScript == null)
                {
                    m_MachineScript = hit.transform.gameObject.GetComponent<Machine>();
                }
                m_NameText.text = m_MachineScript.m_MachineName.ToString();
                m_HealthText.text = "[HEALTH] " + m_MachineScript.m_MachineHealth.ToString("F0") + "/" + m_MachineScript.m_MaxMachineHealth.ToString("F0");
                m_EnergyText.text = "[ENERGY] " + m_MachineScript.m_MachineEnergy.ToString("F0") + "/100";
                m_EnergyConsumptionText.text = "[USAGE] " + m_MachineScript.m_EnergyConsumption.ToString("F0") + "/S";
                m_SpeedText.text = "[SPEED] " + m_MachineScript.m_Speed.ToString("F0") + "/MIN";
            }
            if (hit.transform.gameObject.tag == "Turret")
            {
                if (m_MachineScript == null)
                {
                    m_TurretScript = hit.transform.gameObject.GetComponent<Turret>();
                }
                m_NameText.text = "[TURRET]";
                m_HealthText.text = "[HEALTH] " + m_TurretScript.m_Health.ToString("F0") + "/" + m_TurretScript.m_MaxHealth.ToString("F0");
                m_EnergyText.text = "[DAMAGE] " + m_TurretScript.m_Damage.ToString("F0");
                m_EnergyConsumptionText.text = "[FIRERATE] " + m_TurretScript.m_FireRate.ToString() + "/S";
                m_SpeedText.text = "";
            }

            if(hit.transform.tag != "Machine" && hit.transform.tag != "Turret")
            {
                m_MachineScript = null;
                m_NameText.text = "";
                m_HealthText.text = "";
                m_EnergyText.text = "";
                m_EnergyConsumptionText.text = "";
                m_SpeedText.text = "";
            }
        }
	}
}