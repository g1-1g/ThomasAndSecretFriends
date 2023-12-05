using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    float _speed = 5.0f;
    float Mspeed;

    [SerializeField]
    float _turnSpeed = 4.0f;

    private Transform _transform;
    private bool _isJumping;
    private float _posY;        //오브젝트의 초기 높이
    private float _gravity;     //중력가속도
    private float _jumpPower;   //점프력
    private float _jumpTime;

    float rotationX;
    float rotationY;
    Transform cameraR;
    private CharacterController characterController;


    void Start()
    {
        characterController = GetComponent<CharacterController>();

        cameraR = Camera.main.transform;
        cameraR.eulerAngles = new Vector3(0, 0, 0);

        Mspeed = _speed;


        //점프
        _transform = transform;
        _isJumping = false;
        _posY = transform.position.y;
        _gravity = 15f;
        _jumpPower = 5.0f;
        _jumpTime = 0.0f;

    }

    void Update()
    {
        if (Input.anyKey)
            Onkeyboard();
        MouseRotation();

        if (_isJumping)
        {
            Jump();
        }
    }
    void MouseRotation()
    {
        rotationY += Input.GetAxis("Mouse X") * _turnSpeed;

        rotationX += -Input.GetAxis("Mouse Y") * _turnSpeed;

        rotationX = Mathf.Clamp(rotationX, -45, 80);


        cameraR.eulerAngles = new Vector3(rotationX, transform.eulerAngles.y, 0);
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, rotationY, 0);

    }

    void Onkeyboard()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += transform.TransformDirection(Vector3.forward * Time.deltaTime * Mspeed);

        }

        if (Input.GetKey(KeyCode.S))
        {
            transform.position += transform.TransformDirection(Vector3.back * Time.deltaTime * Mspeed);
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.position += transform.TransformDirection(Vector3.left * Time.deltaTime * Mspeed);
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.position += transform.TransformDirection(Vector3.right * Time.deltaTime * Mspeed);
        }

        //light On/Off
        if (Input.GetKey(KeyCode.LeftShift))
        {
            Mspeed = _speed * 2;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            Mspeed = _speed;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!_isJumping)
            {
                _isJumping = true;
                _posY = _transform.position.y;
            }
        }
    }

    void Jump()
    {
        float height = (_jumpTime * _jumpTime * (-_gravity) / 2) + (_jumpTime * _jumpPower);
        _transform.position = new Vector3(_transform.position.x, _posY + height, _transform.position.z);
        _jumpTime += Time.deltaTime;

        if (height < 0.0f)
        {
            _isJumping = false;
            _jumpTime = 0.0f;
            _transform.position = new Vector3(_transform.position.x, _posY, _transform.position.z);
        }
    }
}
