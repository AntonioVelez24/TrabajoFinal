using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] private float playerHealth;
    [SerializeField] private float currentSpeed;
    [SerializeField] private float energy;
    [SerializeField] private float cameraSensitivity;
    [SerializeField] private Camera playerCamera;

    private float CameraRotation;
    private float currentYRotation;
    private float currentXRotation;

    private float normalSpeed = 8f;
    private float runningSpeed = 16f;

    private float xDirection;
    private float zDirection;
    private Rigidbody _rigidbody;
    private bool isRunning = false;
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        currentSpeed = normalSpeed;
    }
    // Update is called once per frame
    void Update()
    {
        _rigidbody.velocity = new Vector3(currentSpeed * xDirection, _rigidbody.velocity.y, currentSpeed * zDirection);

        CameraRotation = currentYRotation * cameraSensitivity * Time.deltaTime;
        CameraRotation = Mathf.Clamp(CameraRotation, -45, 45);
        playerCamera.transform.localRotation = Quaternion.Euler(CameraRotation, 0f, 0f);
    }
    void PlayerRotation()
    {

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
        }
        else
        {
            currentSpeed = normalSpeed;
        }
    }
    public void ReadCameraMovement (InputAction.CallbackContext context)
    {
        float mouseX = context.ReadValue<float>() * cameraSensitivity * Time.deltaTime;
        transform.Rotate(Vector3.up * mouseX);
    }
}
