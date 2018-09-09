using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class Enemy : MonoBehaviour 
{
    [SerializeField]private float m_Speed;
    public float m_Health;
    [SerializeField]private float m_Damage;

    [SerializeField]private Transform m_target;

    private BaseHealth m_BaseHealthScript;

    private NavMeshAgent m_Nav;

	void Start () 
    {
        m_target = GameObject.FindGameObjectWithTag("Base").transform;
        m_BaseHealthScript = GameObject.FindGameObjectWithTag("Base").GetComponent<BaseHealth>();
        m_Nav = GetComponent<NavMeshAgent>();
	}
	
	void Update () 
    {
        m_Nav.destination = m_target.position;
        m_Nav.speed = m_Speed;

        if (m_Health <= 0)
        {
            Destroy(this.gameObject);
        }
	}

    void OnTriggerEnter(Collider Other)
    {
        if (Other.gameObject.name == "Base")
        {
            Debug.Log("Collision");
            m_BaseHealthScript.m_CurrentHealth -= m_Damage;
            Destroy(this.gameObject);
        }
    }
}
