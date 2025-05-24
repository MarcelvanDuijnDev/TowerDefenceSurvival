using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Movement_TopDown3D : MonoBehaviour
{
    [Header("Camera")]
    [SerializeField] private Camera m_Camera;
    [SerializeField] private Vector3 m_OffSet;
    [SerializeField] private bool m_LookTowardsPlayer;
    [Header("Player")]
    [SerializeField] private float m_NormalSpeed;
    [SerializeField] private float m_SprintSpeed;

    private Rigidbody m_rb;
    private GameObject m_Player;
    private float m_Speed;

    void Start () 
    {
        m_rb = GetComponent<Rigidbody>();
        m_Player = this.gameObject;
    }
	
	void Update ()
    {
        //Movement
        Vector3 moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
        if (Input.GetKey(KeyCode.LeftShift)) { m_Speed = m_SprintSpeed; } else { m_Speed = m_NormalSpeed; }
        m_rb.linearVelocity = moveInput * m_Speed;

        //Camera
        m_Camera.transform.position = new Vector3(m_Player.transform.position.x + m_OffSet.x, m_Player.transform.position.y + m_OffSet.y, m_Player.transform.position.z + m_OffSet.z);
        if(m_LookTowardsPlayer)
        {
            m_Camera.transform.LookAt(m_Player.transform);
        }

        //Player Rotation
        Ray cameraRay = m_Camera.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float rayLength;
        if (groundPlane.Raycast(cameraRay, out rayLength))
        {
            Vector3 pointToLook = cameraRay.GetPoint(rayLength);
            transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));
        }
    }
}
