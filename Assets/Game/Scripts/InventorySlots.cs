using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlots : MonoBehaviour 
{
    [SerializeField]private Button[] m_Slots = new Button[10];
    [SerializeField]private Sprite m_DefaultSprite;
    public int m_CurrentSlot;

    public Text[] m_ObjAmountText = new Text[10];
    public Sprite[] m_Sprites = new Sprite[10];
    public GameObject[] m_InventorySlotObj = new GameObject[10];
    public GameObject[] m_InventorySlotObjBluePrint = new GameObject[10];
    public int[] m_ObjAmount = new int[10];

    public bool m_Active;

    private PlaceObject placeObjectScript;

    [Header("Tools")]
    public GameObject[] m_Tools = new GameObject[4];
    public ToolGetInfo m_ToolGetInfo;

    [SerializeField]private GameObject m_ChooseTool;
    [SerializeField]private Button[] m_ToolButtons = new Button[4];
    int m_ActiveButton;

	void Start () 
    {
        m_ToolGetInfo = GetComponent<ToolGetInfo>();
        m_ChooseTool.SetActive(false);
        placeObjectScript = GetComponent<PlaceObject>();
        m_CurrentSlot = 1;
        for (int i = 0; i < m_Slots.Length; i++)
        {
            m_Slots[i].interactable = false;
            m_ObjAmountText[i].text = "";
        }
        for (int i = 0; i < m_ToolButtons.Length; i++)
        {
            m_ToolButtons[i].interactable = false;
        }
	}
	
	void Update () 
    {
        SentToText();

        if (Input.GetKeyDown(KeyCode.Alpha1)){SetToSlot(0); m_Active = true;}
        if (Input.GetKeyDown(KeyCode.Alpha2)){SetToSlot(1); m_Active = true;}
        if (Input.GetKeyDown(KeyCode.Alpha3)){SetToSlot(2); m_Active = true;}
        if (Input.GetKeyDown(KeyCode.Alpha4)){SetToSlot(3); m_Active = true;}
        if (Input.GetKeyDown(KeyCode.Alpha5)){SetToSlot(4); m_Active = true;}
        if (Input.GetKeyDown(KeyCode.Alpha6)){SetToSlot(5); m_Active = true;}
        if (Input.GetKeyDown(KeyCode.Alpha7)){SetToSlot(6); m_Active = true;}
        if (Input.GetKeyDown(KeyCode.Alpha8)){SetToSlot(7); m_Active = true;}
        if (Input.GetKeyDown(KeyCode.Alpha9)){SetToSlot(8); m_Active = true;}
        if (Input.GetKeyDown(KeyCode.Alpha0)){SetToSlot(9); m_Active = true;}

        if (m_Active)
        {
            for (int i = 0; i < m_Slots.Length; i++)
            {
                if (m_CurrentSlot == i)
                {
                    m_Slots[i].interactable = true;
                    if (m_ObjAmount[i] <= 0)
                    {
                        m_InventorySlotObj[i] = null;
                        m_InventorySlotObjBluePrint[i] = null;
                        m_Sprites[i] = m_DefaultSprite;
                        m_Slots[i].image.sprite = m_Sprites[i];
                    }
                }
                else
                {
                    m_Slots[i].interactable = false;
                }
            }
        }
        else
        {
            for (int i = 0; i < m_Slots.Length; i++)
            {
                if (m_CurrentSlot == i)
                {
                    m_Slots[i].interactable = false;
                }
            }
        }

        if (Input.GetKey(KeyCode.Q))
        {
            m_ChooseTool.SetActive(true);

            if (Input.GetKeyDown(KeyCode.A)){m_ActiveButton = 0; SetTool(0);} if (Input.GetKeyUp(KeyCode.A)){ m_ActiveButton = 5;}
            if (Input.GetKeyDown(KeyCode.D)){m_ActiveButton = 1; SetTool(1);} if (Input.GetKeyUp(KeyCode.D)){ m_ActiveButton = 5;}
            if (Input.GetKeyDown(KeyCode.W)){m_ActiveButton = 2; SetTool(2);} if (Input.GetKeyUp(KeyCode.W)){ m_ActiveButton = 5;}
            if (Input.GetKeyDown(KeyCode.S)){m_ActiveButton = 3; SetTool(3);} if (Input.GetKeyUp(KeyCode.S)){ m_ActiveButton = 5;}

            for (int i = 0; i < m_ToolButtons.Length; i++)
            {
                if (m_ActiveButton == i)
                {
                    m_ToolButtons[i].interactable = true;
                }
                else
                {
                    m_ToolButtons[i].interactable = false;
                }
            }

        }
        else
        {
            m_ActiveButton = 5;
            m_ChooseTool.SetActive(false);
        }
	}

    private void SetTool(int value)
    {
        for (int i = 0; i < m_Slots.Length; i++)
        {
            if (m_CurrentSlot == i)
            {
                m_Slots[i].interactable = false;
            }
        }
        if (m_Tools[value].activeSelf)
        {
            m_Tools[value].SetActive(false);
        }
        else
        {
            m_Tools[value].SetActive(true);
        }
        if (value == 0)
        {
            m_ToolGetInfo.enabled = true;
        }
        else
        {
            m_ToolGetInfo.enabled = false;
        }
    }

    private void SetToSlot(int slotID)
    {
        m_CurrentSlot = slotID;
        for (int i = 0; i < m_Slots.Length; i++)
        {
            if (i == slotID)
            {
                if (m_InventorySlotObj[i] != null)
                {
                    placeObjectScript.m_PrefabObj = m_InventorySlotObj[i];
                    placeObjectScript.m_PrefabObjBluePrint = m_InventorySlotObjBluePrint[i];
                }
            }
        }
    }

    public void AddItemToInventory(int amount, GameObject item, GameObject itemBluePrint, Sprite itemSprite)
    {
        for (int i = 0; i < m_Slots.Length; i++)
        {
            if (m_InventorySlotObj[i] == null || m_InventorySlotObj[i].gameObject == item)
            {
                m_InventorySlotObj[i] = item;
                m_InventorySlotObjBluePrint[i] = itemBluePrint;
                m_ObjAmount[i] += amount;
                m_Sprites[i] = itemSprite;
                m_Slots[i].image.sprite = itemSprite;
                break;
            }
        }
    }

    void SentToText()
    {
        for (int i = 0; i < m_Slots.Length; i++)
        {
            m_ObjAmountText[i].text = m_ObjAmount[i].ToString();
        }
    }
}
