using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeCam : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float m_MouseSensitivity;
    [SerializeField] private float m_CameraSpeed;
    [SerializeField] private float m_ShiftSpeed;

    private float rotationX, rotationY, m_Speed;
	
	void Update ()
    {
        Vector3 moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
        if (Input.GetKey(KeyCode.LeftShift)) { m_Speed = m_ShiftSpeed; } else { m_Speed = m_CameraSpeed; }
        transform.Translate(moveInput * m_Speed * Time.deltaTime);

        rotationX += Input.GetAxis("Mouse X") * m_MouseSensitivity * Time.deltaTime;
        rotationY += Input.GetAxis("Mouse Y") * m_MouseSensitivity * Time.deltaTime;

        transform.eulerAngles = new Vector3(-rotationY, rotationX, 0);
    }
}
