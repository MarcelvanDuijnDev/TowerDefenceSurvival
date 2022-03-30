using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaceObject : MonoBehaviour 
{
    [Header("Settings")]
    public GameObject _PrefabObj;
    public GameObject _PrefabObjBluePrint;
    public GameObject _ShootPos;
    [SerializeField] private GameObject _RotationObj;
    [SerializeField] private Text _InfoText;
    [SerializeField] private float _Range_Turret, _Range_Miners;

    private Vector3 _Rot;
    private InventorySlots _SlotsScript;
    private GameObject _PrefabExample;

	void Start () 
    {
        _SlotsScript = GetComponent<InventorySlots>();
        _InfoText.text = "";
	}
	
	void Update ()
    {
        _PrefabObj = _SlotsScript._InventorySlotObj[_SlotsScript._CurrentSlotID];
        _PrefabObjBluePrint = _SlotsScript._InventorySlotBlueprint[_SlotsScript._CurrentSlotID];

        _Rot.x = _ShootPos.transform.rotation.x;
        _Rot.y = _ShootPos.transform.rotation.y;
        _Rot.z = _ShootPos.transform.rotation.z;
        _Rot = Vector3.forward;

        RaycastHit hit;

        if (_PrefabObj != null)
        {
            //Miners
            if (Physics.Raycast(_ShootPos.transform.position, _ShootPos.transform.TransformDirection(_Rot), out hit, _Range_Miners))
            {
                Debug.DrawRay(_ShootPos.transform.position, _ShootPos.transform.TransformDirection(_Rot) * hit.distance, Color.black);
                if (_SlotsScript._InventorySlotObj[_SlotsScript._CurrentSlotID].tag == "Machine")
                {
                    if (hit.transform.gameObject.tag == "Resources")
                    {
                        if (_PrefabExample == null)
                        {
                            _PrefabExample = Instantiate(_PrefabObjBluePrint, _ShootPos.transform.position, Quaternion.identity);
                        }
                        else
                        {
                            _PrefabExample.transform.position = hit.point;
                            Vector3 rot2 = new Vector3(-90 + _RotationObj.transform.rotation.eulerAngles.x, _RotationObj.transform.rotation.eulerAngles.y, 0);
                            _PrefabExample.transform.eulerAngles = rot2;
                        }
                    }
                    else
                    {
                        Destroy(_PrefabExample);
                    }
                }
            }
            //Turrets
            if (Physics.Raycast(_ShootPos.transform.position, _ShootPos.transform.TransformDirection(_Rot), out hit, _Range_Turret))
            {
                if (_SlotsScript._InventorySlotObj[_SlotsScript._CurrentSlotID].tag == "Turret")
                {
                    if (hit.transform.gameObject.tag == "Ground")
                    {
                        if (_PrefabExample == null)
                        {
                            _PrefabExample = Instantiate(_PrefabObjBluePrint, _ShootPos.transform.position, Quaternion.identity);
                        }
                        else
                        {
                            _PrefabExample.transform.position = hit.point;
                        }
                    }
                    else
                    {
                        Destroy(_PrefabExample);
                    }
                }
            }
            
            if (Input.GetMouseButtonDown(0))
            {
                //Miners
                if (Physics.Raycast(_ShootPos.transform.position, _ShootPos.transform.TransformDirection(_Rot), out hit, _Range_Miners))
                {
                    if (_SlotsScript._InventorySlotObj[_SlotsScript._CurrentSlotID].tag == "Machine")
                    {
                        if (hit.transform.gameObject.tag == "Resources")
                        {
                            Vector3 targetPosition = hit.point;

                            GameObject otherObj = Instantiate(_PrefabObj, _ShootPos.transform.position, Quaternion.identity);
                            Resources resourceScript = hit.transform.gameObject.GetComponent<Resources>();
                            Machine machineScript = otherObj.GetComponent<Machine>();
                            machineScript._Resource = resourceScript;
                            otherObj.transform.position = hit.point;

                            Vector3 rot2 = new Vector3(-90 + _RotationObj.transform.rotation.eulerAngles.x, _RotationObj.transform.rotation.eulerAngles.y, 0);//m_RotationObj.transform.rotation.eulerAngles.y);

                            otherObj.transform.eulerAngles = rot2;

                            _SlotsScript._ObjectAmount[_SlotsScript._CurrentSlotID] -= 1;
                            otherObj = null;
                        }
                    }
                }
                //Turret
                if (Physics.Raycast(_ShootPos.transform.position, _ShootPos.transform.TransformDirection(_Rot), out hit, _Range_Turret))
                {
                    if (_SlotsScript._InventorySlotObj[_SlotsScript._CurrentSlotID].tag == "Turret")
                    {
                        if (hit.transform.gameObject.tag == "Ground")
                        {
                            Vector3 targetPosition = hit.point;

                            GameObject otherObj = Instantiate(_PrefabObj, _ShootPos.transform.position, Quaternion.identity);
                            otherObj.transform.position = hit.point;

                            _SlotsScript._ObjectAmount[_SlotsScript._CurrentSlotID] -= 1;
                            otherObj = null;
                        }
                    }
                }
            }
        }
        else
        {
            _PrefabObj = null;
            Destroy(_PrefabExample);
            _PrefabObjBluePrint = null;
        }
            
        if (Physics.Raycast(_ShootPos.transform.position, _ShootPos.transform.TransformDirection(_Rot), out hit, 3))
        {
            Machine m_MachineScript = null;
            if (hit.transform.gameObject.tag == "Machine")
            {
                m_MachineScript = hit.transform.gameObject.GetComponent<Machine>();
                if (!hit.transform.gameObject.GetComponent<Rigidbody>().isKinematic)
                {
                    _InfoText.text = "E To Pick Up";
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        _SlotsScript.AddItemToInventory(1, m_MachineScript._Obj, m_MachineScript._ObjBlueprint, m_MachineScript._ObjSprite);
                        Destroy(hit.transform.gameObject);
                    }
                }
            }
        }
        else
        {
            _InfoText.text = "";
        }
    }
}
