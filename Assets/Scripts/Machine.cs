using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Machine : MonoBehaviour 
{
    [Header("General info")]
    public string Machine_Name = "[Miner]";
    public float Machine_MaxHealth = 250;
    public float Machine_Energy = 100;
    public float Machine_MaxEnergyConsumption = 1;
    public float Machine_Speed = 100;
    private float _FinalSpeed;
    public string ResourceType = "[STONE]";
    private Rigidbody _RB = null;
    public GameObject _Obj = null;
    public Sprite _ObjSprite = null;
    public GameObject _ObjBlueprint = null;
    private Animator _Animator = null;

    [Header("Extra info")]
    public float _EnergyConsumption;
    public float _MachineHealth;
    public float _CurrentSpeed;
    public float _Timer;
    public float _AnimSpeed;

    public Resources _Resource; // = new Resources();
    public Inventory _InventoryScript;

	void Start () 
    {
        _FinalSpeed = 60 / Machine_Speed;
        _MachineHealth = Machine_MaxHealth;
        _InventoryScript = GameObject.Find("Player").GetComponent<Inventory>();
        _Animator = GetComponent<Animator>();
        _RB = GetComponent<Rigidbody>();
	}
	
	void Update () 
    {
        if (Machine_Energy > 0)
        {
            _Animator.SetBool("Active", true);
            if (Machine_Energy >= Machine_MaxEnergyConsumption)
            {
                Machine_Energy -= Machine_MaxEnergyConsumption * Time.deltaTime;
                _EnergyConsumption = Machine_MaxEnergyConsumption;
            }
            else
            {
                _EnergyConsumption = Machine_Energy / Machine_MaxEnergyConsumption;
                Machine_Energy = 0;
            }
            _CurrentSpeed = _EnergyConsumption / Machine_MaxEnergyConsumption;
            _Timer += _CurrentSpeed / 1 * Time.deltaTime;
            if (_Timer >= _FinalSpeed)
            {
                _Resource.Amount -= 1;
                _InventoryScript.StoneAmount += 1;
                _Timer = 0;
            }
        }
        else
        {
            _Animator.SetBool("Active", false);
        }
        if (_Resource.Amount <= 0)
        {
            _RB.isKinematic = false;
        }
        else
        {
            _RB.isKinematic = true;
        }

        if (Machine_Energy < 100)
        {
            Machine_Energy += 10 * Time.deltaTime;
        }
	}

    public void AddResourse(Resources resource)
    {
        _Resource = resource;
    }
}
