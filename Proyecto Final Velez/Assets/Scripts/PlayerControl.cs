using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerControl : MonoBehaviour
{
    [SerializeField] private float playerHealth;
    [SerializeField] private float currentSpeed;
    [SerializeField] private float energy;
    [SerializeField] private float cameraSensitivity;
    [SerializeField] private Camera playerCamera;

    private float PlayerRotation;
    private float CameraRotation;
    private float currentYRotation;
    private float currentXRotation;

    private float normalSpeed = 8f;
    private float runningSpeed = 16f;

    private float xDirection;
    private float zDirection;
    private Rigidbody _rigidbody;
    private bool isRunning = false;

    public event Action<Vector2> OnCameraMovement;

    private void OnEnable()
    {
        OnCameraMovement += PlayerXRotation;
    }

    private void OnDisable()
    {
        OnCameraMovement -= PlayerXRotation;
    }
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        currentSpeed = normalSpeed;
        PlayerRotation = 0f;
        CameraRotation = 0f;
    }

    void Update()
    {
        _rigidbody.velocity = new Vector3(currentSpeed * xDirection, _rigidbody.velocity.y, currentSpeed * zDirection);

        CameraRotation = currentYRotation * cameraSensitivity * Time.deltaTime;
        CameraRotation = Mathf.Clamp(CameraRotation, -45, 45);
        playerCamera.transform.localRotation = Quaternion.Euler(CameraRotation, 0f, 0f);
    }
    void PlayerXRotation(Vector2 cameraMovement)
    {
        currentXRotation = cameraMovement.x * cameraSensitivity * Time.deltaTime;
        currentXRotation = Mathf.Clamp(currentXRotation, -45f, 45f);
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