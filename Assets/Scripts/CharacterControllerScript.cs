using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControllerScript : MonoBehaviour {

    //Movement
    [Header("Settings")]
    [SerializeField] private float _NormalSpeed = 10;
    [SerializeField] private float _SprintSpeed = 15;
    [SerializeField] private float _JumpSpeed = 5;
    [SerializeField] private float _Gravity = 10;
    [SerializeField] private float _CameraSensitivity = 90;
    [SerializeField] private Transform _Head = null;

    //Private Variables
    private float _RotX = 0.0f;
    private float _RotY = 0.0f;
    private float _CurrentSpeed;
    private CharacterController _CC;
    private Vector3 _MoveDirection = Vector3.zero;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        _CC = GetComponent<CharacterController>();
    }

    void Update() 
    {
        //Look around
        _RotX += Input.GetAxis("Mouse X") * _CameraSensitivity * Time.deltaTime;
        _RotY += Input.GetAxis("Mouse Y") * _CameraSensitivity * Time.deltaTime;
        _RotY = Mathf.Clamp (_RotY, -90, 90);

        transform.localRotation = Quaternion.AngleAxis(_RotX, Vector3.up);
        _Head.transform.localRotation = Quaternion.AngleAxis(_RotY, Vector3.left);

        //Movement
        if (_CC.isGrounded) {
            _MoveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            _MoveDirection = transform.TransformDirection(_MoveDirection);
            _MoveDirection *= _CurrentSpeed;
            if (Input.GetButton("Jump"))
                _MoveDirection.y = _JumpSpeed;
        }
        _MoveDirection.y -= _Gravity * Time.deltaTime;
        _CC.Move(_MoveDirection * Time.deltaTime);

        //Sprint
        if(Input.GetKey(KeyCode.LeftShift))
            _CurrentSpeed = _SprintSpeed;
        else
            _CurrentSpeed = _NormalSpeed;
    }
}