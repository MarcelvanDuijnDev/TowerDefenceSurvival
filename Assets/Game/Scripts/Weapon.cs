using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour 
{
    public float m_Damage;
    public GameObject m_ShootPos;
    Vector3 rot;
	
	void Update () 
    {
        rot.x = m_ShootPos.transform.rotation.x;
        rot.y = m_ShootPos.transform.rotation.y;
        rot.z = m_ShootPos.transform.rotation.z;
        rot = Vector3.forward;

        RaycastHit hit;

        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(m_ShootPos.transform.position, m_ShootPos.transform.TransformDirection(rot), out hit, 1000))
            {
                if (hit.transform.gameObject.tag == "Enemy")
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        hit.transform.gameObject.GetComponent<Enemy>().m_Health -= m_Damage;
                    }
                }
            }
        }
    }
}