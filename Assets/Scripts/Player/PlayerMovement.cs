using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _movementSpeed;
    [SerializeField] private Joystick _joystick;
    [SerializeField] private GameObject _camera;
    [SerializeField] private GameObject _cameraFollowPoint;
    private Rigidbody _playerRb;
    private Vector3 _move;
    private Vector3 _cameraDirection;
    private float _currentVelocity;

    [Header("Animations")]
    [SerializeField] private Animator _characterAnimator;


    public bool _pcInput;

    public void Start()
    {
        _playerRb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void FixedUpdate()
    {
        MoveCharacter();
        
    }
    private void LateUpdate()
    {
        MoveCameraFollowPoint();
    }

    private void Update()
    {
        if (_pcInput)
        {
            /// PC Input ///
            PCInputData();

        }
        else
        {
            /// Phone Input ///
            InputData();
        }

        if (_move.x != 0 || _move.z != 0)
        {
            _characterAnimator.SetBool("isRunning", true);
        }
        else
        {
            _characterAnimator.SetBool("isRunning", false);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(0);
        }
    }

    void InputData()
    {
        _move.x = _joystick.Horizontal;
        _move.z = _joystick.Vertical;
    }
    void MoveCharacter()
    {
        _move = _move.normalized;
        _cameraDirection = _camera.transform.forward;
        _cameraDirection.y = 0;
        _cameraDirection = _cameraDirection.normalized;

        Vector3 moveDirection = _camera.transform.right * _move.x + _cameraDirection * _move.z;

        Vector3 currentPlayerVelocity = _playerRb.velocity;
        currentPlayerVelocity.y = 0;

        _playerRb.AddForce(moveDirection * _movementSpeed - currentPlayerVelocity, ForceMode.VelocityChange);
        RotateCharacter();


    }

    void RotateCharacter()
    {
        if (_move.x != 0 || _move.z != 0)
        {
            Vector3 cameraDirection = _camera.transform.forward;
            cameraDirection.y = 0;
            cameraDirection = cameraDirection.normalized;

            Vector3 moveDirection = _camera.transform.right * _move.x + cameraDirection * _move.z;
            float targetAngle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg;
            float smoothAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _currentVelocity, 0.1f);
            transform.rotation = Quaternion.Euler(0, smoothAngle, 0);
        }
    }

    private void MoveCameraFollowPoint()
    {
        _cameraFollowPoint.transform.position = new Vector3(_playerRb.transform.position.x, _playerRb.transform.position.y + 1.236f, _playerRb.transform.position.z);
    }

    void PCInputData()
    {
        _move.x = Input.GetAxisRaw("Horizontal");
        _move.z = Input.GetAxisRaw("Vertical");
    }



}
