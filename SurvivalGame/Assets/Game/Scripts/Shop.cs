using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour 
{
    public GameObject m_ShootPos;
    public Button[] m_Buttons = new Button[16];
    public GameObject[] m_Objects = new GameObject[16];
    public GameObject[] m_ObjectsBlueprint = new GameObject[16];
    public Sprite[] m_Sprite = new Sprite[16];
    public float[] m_Cost = new float[16];
    public Text[] m_CostText = new Text[16];
    public Text m_Amount;
    Vector3 rot;

    Inventory m_InventoryScript;
    InventorySlots m_InventorySlotScript;

	void Start () 
    {
        m_InventoryScript = GetComponent<Inventory>();
        m_InventorySlotScript = GetComponent<InventorySlots>();
        for (int i = 0; i < m_Buttons.Length; i++)
        {
            m_Buttons[i].interactable = false;
        }
	}
	
	void Update () 
    {
        m_Amount.text = m_InventoryScript.m_StoneAmount.ToString();

        rot.x = m_ShootPos.transform.rotation.x;
        rot.y = m_ShootPos.transform.rotation.y;
        rot.z = m_ShootPos.transform.rotation.z;
        rot = Vector3.forward;

        RaycastHit hit;

        if (Physics.Raycast(m_ShootPos.transform.position, m_ShootPos.transform.TransformDirection(rot), out hit, 5))
        {
            for (int i = 0; i < m_Buttons.Length; i++)
            {
                if (hit.transform.gameObject == m_Buttons[i].gameObject)
                {
                    m_Buttons[i].interactable = true;
                    if (m_InventoryScript.m_StoneAmount >= m_Cost[i])
                    {
                        if (Input.GetKeyDown(KeyCode.E))
                        {
                            m_InventoryScript.m_StoneAmount -= m_Cost[i];
                            m_InventorySlotScript.AddItemToInventory(1, m_Objects[i], m_ObjectsBlueprint[i], m_Sprite[i]);
                            if (i == 0){m_Cost[i] = Mathf.RoundToInt(m_Cost[i] * 1.5f);}
                            if (i == 1){m_Cost[i] = Mathf.RoundToInt(m_Cost[i] * 1.8f);}
                            for (int o = 0; o < m_Cost.Length; o++) 
                            {
                                Mathf.RoundToInt(m_Cost[o]);
                            }
                        }
                    }
                }
                else
                {
                    m_Buttons[i].interactable = false;
                }
            }
        }

        for (int i = 0; i < m_Cost.Length; i++)
        {
            m_CostText[i].text = m_Cost[i].ToString();
        }
	}
}
