using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour 
{
    public GameObject _ShootPos;
    public Button[] _Buttons = new Button[16];
    public GameObject[] _Objects = new GameObject[16];
    public GameObject[] _ObjectsBlueprint = new GameObject[16];
    public Sprite[] _Sprite = new Sprite[16];
    public float[] _Cost = new float[16];
    public Text[] _CostText = new Text[16];
    public Text _Amount;

    private Vector3 _Rot;
    private Inventory _InventoryScript;
    private InventorySlots _InventorySlotScript;

	void Start () 
    {
        _InventoryScript = GetComponent<Inventory>();
        _InventorySlotScript = GetComponent<InventorySlots>();
        for (int i = 0; i < _Buttons.Length; i++)
        {
            _Buttons[i].interactable = false;
        }
	}
	
	void Update () 
    {
        _Amount.text = _InventoryScript.StoneAmount.ToString();

        _Rot.x = _ShootPos.transform.rotation.x;
        _Rot.y = _ShootPos.transform.rotation.y;
        _Rot.z = _ShootPos.transform.rotation.z;
        _Rot = Vector3.forward;

        RaycastHit hit;

        if (Physics.Raycast(_ShootPos.transform.position, _ShootPos.transform.TransformDirection(_Rot), out hit, 5))
        {
            for (int i = 0; i < _Buttons.Length; i++)
            {
                if (hit.transform.gameObject == _Buttons[i].gameObject)
                {
                    _Buttons[i].interactable = true;
                    if (_InventoryScript.StoneAmount >= _Cost[i])
                    {
                        if (Input.GetKeyDown(KeyCode.E))
                        {
                            _InventoryScript.StoneAmount -= _Cost[i];
                            _InventorySlotScript.AddItemToInventory(1, _Objects[i], _ObjectsBlueprint[i], _Sprite[i]);
                            if (i == 0){_Cost[i] = Mathf.RoundToInt(_Cost[i] * 1.5f);}
                            if (i == 1){_Cost[i] = Mathf.RoundToInt(_Cost[i] * 1.8f);}
                            for (int o = 0; o < _Cost.Length; o++) 
                            {
                                Mathf.RoundToInt(_Cost[o]);
                            }
                        }
                    }
                }
                else
                {
                    _Buttons[i].interactable = false;
                }
            }
        }

        for (int i = 0; i < _Cost.Length; i++)
        {
            _CostText[i].text = _Cost[i].ToString();
        }
	}
}
