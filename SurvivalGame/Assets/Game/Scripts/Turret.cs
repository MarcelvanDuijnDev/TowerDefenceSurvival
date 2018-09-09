using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour 
{
    public GameObject m_Target;
    public float m_DistanceMin;
    public float m_DistanceMax;

    public float m_MaxHealth;
    public float m_Health;

    public float m_FireRate;
    private float m_FinalFireRate;
    public float m_Damage;
    [SerializeField]private GameObject m_ShootPart;
    private float m_Timer;

	void Start () 
    {
        m_FinalFireRate = 60 / m_FireRate;
        m_Health = m_MaxHealth;
	}
	
	void Update () 
    {
        if (m_Target != null)
        {
            m_ShootPart.transform.LookAt(m_Target.transform.position);
            m_Timer += 1 * Time.deltaTime;
            if (m_Timer >= m_FinalFireRate)
            {
                m_Target.GetComponent<Enemy>().m_Health -= m_Damage;
                m_Timer = 0;
            }
        }
        else
        {
            m_ShootPart.transform.rotation = Quaternion.Euler(90,0,0);
        }

        m_Target = FindEnemy(m_DistanceMin,m_DistanceMax);
	}

    public GameObject FindEnemy(float min, float max)
    {
        GameObject[] m_Targets = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;

        min = min * min;
        max = max * max;
        foreach (GameObject target in m_Targets)
        {
            Vector3 diff = target.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance && curDistance >= min && curDistance <= max)
            {
                closest = target;
                distance = curDistance;
            }
        }
        return closest;
    }
}
