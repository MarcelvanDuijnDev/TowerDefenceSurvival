using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement_Platformer3D : MonoBehaviour
{
    [Header("player")]
    [SerializeField] private float m_NormalSpeed;
    [SerializeField] private float m_SprintSpeed;
    [SerializeField] private float m_JumpSpeed;
    [SerializeField] private float m_Gravity;
    private Vector3 m_MoveDirection;

    [Header("Camera")]
    [SerializeField] private GameObject m_Camera;
    [SerializeField] private Vector3 m_OffSet;
    [SerializeField] private bool m_LookTowardsPlayer;

    private float m_Speed;
    private CharacterController m_CC;
    private GameObject m_Player;

    void Start ()
    {
        Cursor.lockState = CursorLockMode.Locked;
        m_CC = GetComponent<CharacterController>();
        m_Player = this.gameObject;
    }
	
	void Update ()
    {
        //Movement
        if (m_CC.isGrounded)
        {
            m_MoveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, 0);
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

        //Camera
        m_Camera.transform.position = new Vector3(m_Player.transform.position.x + m_OffSet.x, m_Player.transform.position.y + m_OffSet.y, m_Player.transform.position.z + m_OffSet.z);
        if (m_LookTowardsPlayer)
        {
            m_Camera.transform.LookAt(m_Player.transform);
        }
    }
}
