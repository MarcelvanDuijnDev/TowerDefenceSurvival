using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Movement_ThirdPerson : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] private float m_NormalSpeed;
    [SerializeField] private float m_SprintSpeed;
    [SerializeField] private float m_MouseSensitivity;
    [SerializeField] private float m_JumpSpeed;
    [SerializeField] private float m_Gravity;

    [Header("Camera")]
    [SerializeField] private GameObject m_RotationPoint;

    private CharacterController m_CC;
    private GameObject m_Player;
    private float m_Speed;
    private Vector3 m_MoveDirection;

    //Rotation
    private float m_RotationX = 0.0f;
    private float m_RotationY = 0.0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        m_CC = GetComponent<CharacterController>();
        m_Player = this.gameObject;
    }

    void Update()
    {
        //Movement
        if (m_CC.isGrounded)
        {
            m_MoveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            m_MoveDirection = transform.TransformDirection(m_MoveDirection);
            m_MoveDirection *= m_Speed;
            if (Input.GetButton("Jump"))
                m_MoveDirection.y = m_JumpSpeed;
        }
        m_MoveDirection.y -= m_Gravity * Time.deltaTime;
        m_CC.Move(m_MoveDirection * Time.deltaTime);

        //Sprint
        if (Input.GetKey(KeyCode.LeftShift))
        {
            m_Speed = m_SprintSpeed;
        }
        else
        {
            m_Speed = m_NormalSpeed;
        }

        //Player Rotation
        m_RotationX += Input.GetAxis("Mouse X") * m_MouseSensitivity * Time.deltaTime;
        m_RotationY += Input.GetAxis("Mouse Y") * m_MouseSensitivity * Time.deltaTime;
        m_RotationY = Mathf.Clamp(m_RotationY, -90, 90);

        transform.localRotation = Quaternion.AngleAxis(m_RotationX, Vector3.up);
        m_RotationPoint.transform.localRotation = Quaternion.AngleAxis(m_RotationY, Vector3.left);
    }
}
