using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement_ThirdPerson : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] private float m_MovementSpeed;
    [SerializeField] private float m_SprintSpeed;
    [SerializeField] private float m_MouseSensitivity;

    [Header("Camera")]
    [SerializeField] private GameObject m_RotationPoint;

    private Rigidbody m_rb;
    private GameObject m_Player;
    private float m_Speed;

    //Rotation
    private float rotationX = 0.0f;
    private float rotationY = 0.0f;

    void Start()
    {
        m_rb = GetComponent<Rigidbody>();
        m_Player = this.gameObject;
    }

    void Update()
    {
        //Movement
        Vector3 moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
        if (Input.GetKey(KeyCode.LeftShift)) { m_Speed = m_SprintSpeed; } else { m_Speed = m_MovementSpeed; }
        m_rb.velocity = moveInput * m_Speed;

        //Player Rotation
        rotationX += Input.GetAxis("Mouse X") * m_MouseSensitivity * Time.deltaTime;
        rotationY += Input.GetAxis("Mouse Y") * m_MouseSensitivity * Time.deltaTime;
        rotationY = Mathf.Clamp(rotationY, -90, 90);

        transform.localRotation = Quaternion.AngleAxis(rotationX, Vector3.up);
        m_RotationPoint.transform.localRotation = Quaternion.AngleAxis(rotationY, Vector3.left);
    }
}
