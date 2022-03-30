using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlots : MonoBehaviour 
{
    [Header("Settings")]
    public Sprite _DefaultSprite = null;
    public int _CurrentSlotID = 9;

    //Inv Slots
    public Button[] _InvSlots = new Button[10];
    public Text[] _ObjAmountText = new Text[10];
    public Sprite[] _ItemSprites = new Sprite[10];
    public GameObject[] _InventorySlotObj = new GameObject[10];
    public GameObject[] _InventorySlotBlueprint = new GameObject[10];
    public int[] _ObjectAmount = new int[10];

    public bool _Active;
    private PlaceObject _PlaceObjScript;

    [Header("Tools")]
    public GameObject[] _Tools = new GameObject[4];
    public ToolGetInfo _ToolGetInfo;

    [SerializeField]private GameObject _ChooseTool;
    [SerializeField]private Button[] _ToolButtons = new Button[4];
    int _ActiveButton;

	void Start () 
    {
        _ToolGetInfo = GetComponent<ToolGetInfo>();
        _ChooseTool.SetActive(false);
        _PlaceObjScript = GetComponent<PlaceObject>();
        _CurrentSlotID = 9;
        for (int i = 0; i < _InvSlots.Length; i++)
        {
            _InvSlots[i].interactable = false;
            _ObjAmountText[i].text = "";
        }
        for (int i = 0; i < _ToolButtons.Length; i++)
        {
            _ToolButtons[i].interactable = false;
        }
	}
	
	void Update () 
    {
        SentToText();

        if (Input.GetKeyDown(KeyCode.Alpha1)){SetToSlot(0); _Active = true;}
        if (Input.GetKeyDown(KeyCode.Alpha2)){SetToSlot(1); _Active = true;}
        if (Input.GetKeyDown(KeyCode.Alpha3)){SetToSlot(2); _Active = true;}
        if (Input.GetKeyDown(KeyCode.Alpha4)){SetToSlot(3); _Active = true;}
        if (Input.GetKeyDown(KeyCode.Alpha5)){SetToSlot(4); _Active = true;}
        if (Input.GetKeyDown(KeyCode.Alpha6)){SetToSlot(5); _Active = true;}
        if (Input.GetKeyDown(KeyCode.Alpha7)){SetToSlot(6); _Active = true;}
        if (Input.GetKeyDown(KeyCode.Alpha8)){SetToSlot(7); _Active = true;}
        if (Input.GetKeyDown(KeyCode.Alpha9)){SetToSlot(8); _Active = true;}
        if (Input.GetKeyDown(KeyCode.Alpha0)){SetToSlot(9); _Active = true;}

        if (_Active)
        {
            for (int i = 0; i < _InvSlots.Length; i++)
            {
                if (_CurrentSlotID == i)
                {
                    _InvSlots[i].interactable = true;
                    if (_ObjectAmount[i] <= 0)
                    {
                        _InventorySlotObj[i] = null;
                        _InventorySlotBlueprint[i] = null;
                        _ItemSprites[i] = _DefaultSprite;
                        _InvSlots[i].image.sprite = _ItemSprites[i];
                    }
                }
                else
                {
                    _InvSlots[i].interactable = false;
                }
            }
        }
        else
        {
            for (int i = 0; i < _InvSlots.Length; i++)
            {
                if (_CurrentSlotID == i)
                {
                    _InvSlots[i].interactable = false;
                }
            }
        }

        if (Input.GetKey(KeyCode.Q))
        {
            _ChooseTool.SetActive(true);

            if (Input.GetKeyDown(KeyCode.A)){_ActiveButton = 0; SetTool(0);} if (Input.GetKeyUp(KeyCode.A)){ _ActiveButton = 5;}
            if (Input.GetKeyDown(KeyCode.D)){_ActiveButton = 1; SetTool(1);} if (Input.GetKeyUp(KeyCode.D)){ _ActiveButton = 5;}
            if (Input.GetKeyDown(KeyCode.W)){_ActiveButton = 2; SetTool(2);} if (Input.GetKeyUp(KeyCode.W)){ _ActiveButton = 5;}
            if (Input.GetKeyDown(KeyCode.S)){_ActiveButton = 3; SetTool(3);} if (Input.GetKeyUp(KeyCode.S)){ _ActiveButton = 5;}

            for (int i = 0; i < _ToolButtons.Length; i++)
            {
                if (_ActiveButton == i)
                {
                    _ToolButtons[i].interactable = true;
                }
                else
                {
                    _ToolButtons[i].interactable = false;
                }
            }

        }
        else
        {
            _ActiveButton = 5;
            _ChooseTool.SetActive(false);
        }
	}

    private void SetTool(int value)
    {
        for (int i = 0; i < _InvSlots.Length; i++)
        {
            if (_CurrentSlotID == i)
            {
                _InvSlots[i].interactable = false;
            }
        }
        if (_Tools[value].activeSelf)
        {
            _Tools[value].SetActive(false);
        }
        else
        {
            _Tools[value].SetActive(true);
        }
        if (value == 0)
        {
            _ToolGetInfo.enabled = true;
        }
        else
        {
            _ToolGetInfo.enabled = false;
        }
    }

    private void SetToSlot(int slotID)
    {
        _CurrentSlotID = slotID;
        for (int i = 0; i < _InvSlots.Length; i++)
        {
            if (i == slotID)
            {
                if (_InventorySlotObj[i] != null)
                {
                    _PlaceObjScript._PrefabObj = _InventorySlotObj[i];
                    _PlaceObjScript._PrefabObjBluePrint = _InventorySlotBlueprint[i];
                }
            }
        }
    }

    public void AddItemToInventory(int amount, GameObject item, GameObject itemBluePrint, Sprite itemSprite)
    {
        for (int i = 0; i < _InvSlots.Length; i++)
        {
            if (_InventorySlotObj[i] == null || _InventorySlotObj[i].gameObject == item)
            {
                _InventorySlotObj[i] = item;
                _InventorySlotBlueprint[i] = itemBluePrint;
                _ObjectAmount[i] += amount;
                _ItemSprites[i] = itemSprite;
                _InvSlots[i].image.sprite = itemSprite;
                break;
            }
        }
    }

    void SentToText()
    {
        for (int i = 0; i < _InvSlots.Length; i++)
        {
            _ObjAmountText[i].text = _ObjectAmount[i].ToString();
        }
    }
}
