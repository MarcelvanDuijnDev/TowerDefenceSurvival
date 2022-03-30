using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Movement_Fps : MonoBehaviour {

    [Header("Player")]
    [SerializeField] private float m_NormalSpeed;
    [SerializeField] private float m_SprintSpeed;
    [SerializeField] private float m_JumpSpeed;
    [SerializeField] private float m_Gravity;

    [Header("Camera")]
    [SerializeField] private Transform head;
    [SerializeField] private float cameraSensitivity;

    private CharacterController m_CC;
    private Vector3 m_MoveDirection;
    private float m_Speed;

    //Rotation
    private float m_RotationX = 0.0f;
    private float m_RotationY = 0.0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        m_CC = GetComponent<CharacterController>();
    }

    void Update() 
    {
        //Rotation
        m_RotationX += Input.GetAxis("Mouse X") * cameraSensitivity * Time.deltaTime;
        m_RotationY += Input.GetAxis("Mouse Y") * cameraSensitivity * Time.deltaTime;
        m_RotationY = Mathf.Clamp (m_RotationY, -90, 90);

        transform.localRotation = Quaternion.AngleAxis(m_RotationX, Vector3.up);
        head.transform.localRotation = Quaternion.AngleAxis(m_RotationY, Vector3.left);

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
    }
}
