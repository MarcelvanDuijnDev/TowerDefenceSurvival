using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    [SerializeField] private Vector3 m_Rotation;

	void Update ()
    {
        this.transform.Rotate(m_Rotation.x * Time.deltaTime, m_Rotation.y * Time.deltaTime, m_Rotation.z * Time.deltaTime);
    }
}
