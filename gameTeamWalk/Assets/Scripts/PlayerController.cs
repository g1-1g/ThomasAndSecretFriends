using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
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


    [SerializeField] AudioClip walk;
    [SerializeField] AudioClip run;

    [SerializeField] AudioSource audioSource;

    private bool _isCursorVisible;

    public bool IsCursorVisible
    {
        get { return _isCursorVisible; }
        set
        {
            _isCursorVisible = value;
            switch (_isCursorVisible)
            {
                case true:
                    UnityEngine.Cursor.visible = false;
                    break;
                case false:
                    UnityEngine.Cursor.visible = true;
                    break;
            }
        }
    }

    void Start()
    {
        cameraR = Camera.main.transform;
        cameraR.eulerAngles = new Vector3(0, 0, 0);

        Mspeed = _speed;

        //점프
        _transform = transform;
        _isJumping = false;
        _posY = transform.position.y;
        _gravity = 40f;
        _jumpPower = 15.0f;
        _jumpTime = 0.0f;

        audioSource.clip = null;

    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.C))
        {
            if (IsCursorVisible)
            {
                IsCursorVisible = false;
            }
            else
            {
                IsCursorVisible = true;
            }

        }
        if (!Physics.Raycast(transform.position + Vector3.up * 0.5f, transform.forward, 1.0f, LayerMask.GetMask("Block"))&& !Physics.Raycast(transform.position + Vector3.up * 0.5f, transform.right, 1.0f, LayerMask.GetMask("Block"))
            && !Physics.Raycast(transform.position + Vector3.up * 0.5f, -transform.forward, 1.0f, LayerMask.GetMask("Block"))&& !Physics.Raycast(transform.position + Vector3.up * 0.5f, -transform.right, 1.0f, LayerMask.GetMask("Block")))
        {
            if (Input.anyKey)
                Onkeyboard();
            else { audioSource.clip = null; }
        }
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
        if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)) && (Input.GetKey(KeyCode.LeftShift) && !Input.GetKeyDown(KeyCode.Space)))
        {
            if (audioSource.clip != run)
            {
                audioSource.clip = run;
                audioSource.Play();
            }
        }
        else
        {
            if (audioSource.clip != walk)
            {
                audioSource.clip = walk;
                audioSource.Play();
            }
        }

        if (Input.GetKey(KeyCode.LeftShift) && !Input.GetKeyDown(KeyCode.Space))
        {

            Mspeed = _speed * 1.5f;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            audioSource.clip = null;
            Mspeed = _speed;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!_isJumping)
            {
                audioSource.clip = null;
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
