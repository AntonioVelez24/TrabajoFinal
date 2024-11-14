using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;
public class PlayerControl : MonoBehaviour
{
    [SerializeField] private float playerHealth;
    [SerializeField] private float currentSpeed;
    [SerializeField] private float energy;
    [SerializeField] private float cameraSensitivity;
    [SerializeField] private Camera playerCamera;

    private float currentYRotation = 0f;
    private float currentXRotation = 0f;

    private float normalSpeed = 8f;
    private float runningSpeed = 16f;

    private float xDirection;
    private float zDirection;
    private Rigidbody _rigidbody;
    private bool isRunning = false;

    public event Action<Vector2> OnCameraMovement;

    private void OnEnable()
    {
        OnCameraMovement += PlayerRotation;
        OnCameraMovement += CameraRotation;
    }

    private void OnDisable()
    {
        OnCameraMovement -= PlayerRotation;
        OnCameraMovement -= CameraRotation;
    }
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        currentSpeed = normalSpeed;
        CameraRotation(Vector2.zero);
    }

    void Update()
    {
        MovePlayer();            
    }
    private void MovePlayer()
    {       
        Vector3 movement = new Vector3(xDirection, transform.position.y , zDirection);
        movement = transform.TransformDirection(movement); 
        _rigidbody.velocity = new Vector3(movement.x * currentSpeed, _rigidbody.velocity.y, movement.z * currentSpeed);
    }
    private void CameraRotation(Vector2 cameraYMovement)
    {
        currentXRotation -= cameraYMovement.y * cameraSensitivity * Time.deltaTime;
        currentXRotation = Mathf.Clamp(currentXRotation, -45f, 45f);
        playerCamera.transform.localRotation = Quaternion.Euler(currentXRotation, 0f, 0f);
    }
    void PlayerRotation(Vector2 cameraXMovement)
    {
        currentYRotation = cameraXMovement.x;
        transform.Rotate(Vector3.up * currentYRotation * cameraSensitivity * Time.deltaTime);
    }
    public void ReadMovementX(InputAction.CallbackContext context)
    {
        xDirection = context.ReadValue<float>();
    }
    public void ReadMovementZ(InputAction.CallbackContext context)
    {
        zDirection = context.ReadValue<float>();
    }
    public void Readinteract(InputAction.CallbackContext context)
    {
        if (context.performed)
        {

        }
    }
    public void ReadRunButton(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            currentSpeed = runningSpeed;
            isRunning = true;
        }
        else
        {
            currentSpeed = normalSpeed;
            isRunning = false;
        }
    }
    public void ReadCameraMovement(InputAction.CallbackContext context)
    {
        Vector2 cameraMovement = context.ReadValue<Vector2>();
        OnCameraMovement?.Invoke(cameraMovement);
    }
}