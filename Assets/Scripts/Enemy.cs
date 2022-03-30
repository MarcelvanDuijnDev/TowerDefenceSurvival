using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class Enemy : MonoBehaviour 
{
    [Header("Settings")]
    [SerializeField] private float _Speed = 10;
    [SerializeField] private float _Health = 100;
    [SerializeField] private float _Damage = 100;

    //Private Variables
    private BaseHealth _BaseHealthScript;
    private NavMeshAgent _NavAgent;
    private float _CurrentHealth;
    private Transform _Target;

    void Start () 
    {
        _Target = GameObject.FindGameObjectWithTag("Base").transform;
        _BaseHealthScript = GameObject.FindGameObjectWithTag("Base").GetComponent<BaseHealth>();
        _NavAgent = GetComponent<NavMeshAgent>();
	}

    private void OnEnable()
    {
        _CurrentHealth = _Health;
    }

    void Update () 
    {
        _NavAgent.destination = _Target.position;
        _NavAgent.speed = _Speed;

        if (_CurrentHealth <= 0)
        {
            this.gameObject.SetActive(false);
        }
	}

    public void DoDamage(float amount)
    {
        _CurrentHealth -= amount;
    }

    void OnTriggerEnter(Collider Other)
    {
        if (Other.gameObject.name == "Base")
        {
            _BaseHealthScript.DoDamage(_Damage);
            transform.gameObject.SetActive(false);
        }
    }
}
