using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaceObject : MonoBehaviour 
{
    [Header("Settings")]
    public GameObject m_PrefabObj;
    public GameObject m_PrefabObjBluePrint;
    public GameObject m_ShootPos;
    [SerializeField]private GameObject m_RotationObj;
    [SerializeField]private Text m_InfoText;
    [SerializeField]private float m_Range_Turret,m_Range_Miners;

    Vector3 rot;
    InventorySlots m_SlotsScript;
    Inventory m_InventoryScript;
    GameObject m_PrefabExample;

	void Start () 
    {
        m_SlotsScript = GetComponent<InventorySlots>();
        m_InventoryScript = GetComponent<Inventory>();
        m_InfoText.text = "";
	}
	
	void Update ()
    {
        m_PrefabObj = m_SlotsScript.m_InventorySlotObj[m_SlotsScript.m_CurrentSlot];
        m_PrefabObjBluePrint = m_SlotsScript.m_InventorySlotObjBluePrint[m_SlotsScript.m_CurrentSlot];

        rot.x = m_ShootPos.transform.rotation.x;
        rot.y = m_ShootPos.transform.rotation.y;
        rot.z = m_ShootPos.transform.rotation.z;
        rot = Vector3.forward;

        RaycastHit hit;

        if (m_PrefabObj != null)
        {
            //Miners
            if (Physics.Raycast(m_ShootPos.transform.position, m_ShootPos.transform.TransformDirection(rot), out hit, m_Range_Miners))
            {
                Debug.DrawRay(m_ShootPos.transform.position, m_ShootPos.transform.TransformDirection(rot) * hit.distance, Color.black);
                if (m_SlotsScript.m_InventorySlotObj[m_SlotsScript.m_CurrentSlot].tag == "Machine")
                {
                    if (hit.transform.gameObject.tag == "Resources")
                    {
                        if (m_PrefabExample == null)
                        {
                            m_PrefabExample = Instantiate(m_PrefabObjBluePrint, m_ShootPos.transform.position, Quaternion.identity);
                        }
                        else
                        {
                            m_PrefabExample.transform.position = hit.point;
                            Vector3 rot2 = new Vector3(-90 + m_RotationObj.transform.rotation.eulerAngles.x, m_RotationObj.transform.rotation.eulerAngles.y, 0);
                            m_PrefabExample.transform.eulerAngles = rot2;
                        }
                    }
                    else
                    {
                        Destroy(m_PrefabExample);
                    }
                }
            }
            //Turrets
            if (Physics.Raycast(m_ShootPos.transform.position, m_ShootPos.transform.TransformDirection(rot), out hit, m_Range_Turret))
            {
                if (m_SlotsScript.m_InventorySlotObj[m_SlotsScript.m_CurrentSlot].tag == "Turret")
                {
                    if (hit.transform.gameObject.tag == "Ground")
                    {
                        if (m_PrefabExample == null)
                        {
                            m_PrefabExample = Instantiate(m_PrefabObjBluePrint, m_ShootPos.transform.position, Quaternion.identity);
                        }
                        else
                        {
                            m_PrefabExample.transform.position = hit.point;
                        }
                    }
                    else
                    {
                        Destroy(m_PrefabExample);
                    }
                }
            }
            
            if (Input.GetMouseButtonDown(0))
            {
                //Miners
                if (Physics.Raycast(m_ShootPos.transform.position, m_ShootPos.transform.TransformDirection(rot), out hit, m_Range_Miners))
                {
                    if (m_SlotsScript.m_InventorySlotObj[m_SlotsScript.m_CurrentSlot].tag == "Machine")
                    {
                        if (hit.transform.gameObject.tag == "Resources")
                        {
                            Vector3 targetPosition = hit.point;

                            GameObject otherObj = Instantiate(m_PrefabObj, m_ShootPos.transform.position, Quaternion.identity);
                            Resources resourceScript = hit.transform.gameObject.GetComponent<Resources>();
                            Machine machineScript = otherObj.GetComponent<Machine>();
                            machineScript.m_Resource = resourceScript;
                            otherObj.transform.position = hit.point;

                            Vector3 rot2 = new Vector3(-90 + m_RotationObj.transform.rotation.eulerAngles.x, m_RotationObj.transform.rotation.eulerAngles.y, 0);//m_RotationObj.transform.rotation.eulerAngles.y);

                            otherObj.transform.eulerAngles = rot2;

                            m_SlotsScript.m_ObjAmount[m_SlotsScript.m_CurrentSlot] -= 1;
                            otherObj = null;
                        }
                    }
                }
                //Turret
                if (Physics.Raycast(m_ShootPos.transform.position, m_ShootPos.transform.TransformDirection(rot), out hit, m_Range_Turret))
                {
                    if (m_SlotsScript.m_InventorySlotObj[m_SlotsScript.m_CurrentSlot].tag == "Turret")
                    {
                        if (hit.transform.gameObject.tag == "Ground")
                        {
                            Vector3 targetPosition = hit.point;

                            GameObject otherObj = Instantiate(m_PrefabObj, m_ShootPos.transform.position, Quaternion.identity);
                            otherObj.transform.position = hit.point;

                            m_SlotsScript.m_ObjAmount[m_SlotsScript.m_CurrentSlot] -= 1;
                            otherObj = null;
                        }
                    }
                }
            }
        }
        else
        {
            m_PrefabObj = null;
            Destroy(m_PrefabExample);
            m_PrefabObjBluePrint = null;
        }
            
        if (Physics.Raycast(m_ShootPos.transform.position, m_ShootPos.transform.TransformDirection(rot), out hit, 3))
        {
            Machine m_MachineScript = null;
            if (hit.transform.gameObject.tag == "Machine")
            {
                m_MachineScript = hit.transform.gameObject.GetComponent<Machine>();
                if (!hit.transform.gameObject.GetComponent<Rigidbody>().isKinematic)
                {
                    m_InfoText.text = "E To Pick Up";
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        m_SlotsScript.AddItemToInventory(1, m_MachineScript.m_Object, m_MachineScript.m_ObjectBluePrint, m_MachineScript.m_ObjectSprite);
                        Destroy(hit.transform.gameObject);
                    }
                }
            }
        }
        else
        {
            m_InfoText.text = "";
        }
    }
}
