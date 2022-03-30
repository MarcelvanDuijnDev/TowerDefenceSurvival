using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolGetInfo : MonoBehaviour 
{
    [SerializeField]private GameObject _ShootPos;
    [SerializeField]private Text _NameText;
    [SerializeField]private Text _HealthText;
    [SerializeField]private Text _EnergyText;
    [SerializeField]private Text _EnergyConsumptionText;
    [SerializeField]private Text _SpeedText;

    private Vector3 _Rot;
    private Machine _MachineScript;
    private Turret _TurretScript;
	
	void Update () 
    {
        _Rot.x = _ShootPos.transform.rotation.x;
        _Rot.y = _ShootPos.transform.rotation.y;
        _Rot.z = _ShootPos.transform.rotation.z;
        _Rot = Vector3.forward;

        RaycastHit hit;

        if (Physics.Raycast(_ShootPos.transform.position, _ShootPos.transform.TransformDirection(_Rot), out hit, 20))
        {
            if (hit.transform.gameObject.tag == "Machine")
            {
                if (_MachineScript == null)
                {
                    _MachineScript = hit.transform.gameObject.GetComponent<Machine>();
                }
                _NameText.text = _MachineScript.Machine_Name.ToString();
                _HealthText.text = "[HEALTH] " + _MachineScript._MachineHealth.ToString("F0") + "/" + _MachineScript.Machine_MaxHealth.ToString("F0");
                _EnergyText.text = "[ENERGY] " + _MachineScript.Machine_Energy.ToString("F0") + "/100";
                _EnergyConsumptionText.text = "[USAGE] " + _MachineScript._EnergyConsumption.ToString("F0") + "/S";
                _SpeedText.text = "[SPEED] " + _MachineScript.Machine_Speed.ToString("F0") + "/MIN";
            }
            if (hit.transform.gameObject.tag == "Turret")
            {
                if (_MachineScript == null)
                {
                    _TurretScript = hit.transform.gameObject.GetComponent<Turret>();
                }
                _NameText.text = "[TURRET]";
                _HealthText.text = "[HEALTH] " + _TurretScript._Health.ToString("F0") + "/" + _TurretScript._MaxHealth.ToString("F0");
                _EnergyText.text = "[DAMAGE] " + _TurretScript._Damage.ToString("F0");
                _EnergyConsumptionText.text = "[FIRERATE] " + _TurretScript._FireRate.ToString() + "/S";
                _SpeedText.text = "";
            }

            if(hit.transform.tag != "Machine" && hit.transform.tag != "Turret")
            {
                _MachineScript = null;
                _NameText.text = "";
                _HealthText.text = "";
                _EnergyText.text = "";
                _EnergyConsumptionText.text = "";
                _SpeedText.text = "";
            }
        }
	}
}