using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;
using UnityEngine.Experimental.GlobalIllumination;
public class PlayerControl : MonoBehaviour
{
    [SerializeField] public float playerHealth;
    [SerializeField] private float currentSpeed;
    [SerializeField] private float energy;
    [SerializeField] private float cameraSensitivity;
    [SerializeField] private Camera playerCamera;
    [SerializeField] private Light playerLight;
    private float currentYRotation = 0f;
    private float currentXRotation = 0f;

    private float normalSpeed = 15f;
    private float runningSpeed = 25f;

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

        LockCursor();

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
        playerLight.transform.localRotation = Quaternion.Euler(currentXRotation, 0f, 0f);
    }
    void PlayerRotation(Vector2 cameraXMovement)
    {
        currentYRotation = cameraXMovement.x;
        transform.Rotate(Vector3.up * currentYRotation * cameraSensitivity * Time.deltaTime);
    }
    private void LockCursor()
    {
        if (Cursor.lockState != CursorLockMode.Locked)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
    private void UnlockCursor()
    {
    Cursor.lockState = CursorLockMode.None;
    Cursor.visible = true;
    }
    private void Colectitem()
    {
        RaycastHit hit;

        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, 15f))
        {
            ItemControl interactable = hit.collider.GetComponent<ItemControl>();

            if (interactable != null)
            {                
                interactable.Interact();
                GameControl.Instance.score += 100;
            }
        }
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
            Colectitem();
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
    public void ReadToggleFlashLight(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            playerLight.enabled = !playerLight.enabled;
        }
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            playerHealth = playerHealth - 10f;
            if (playerHealth <= 0)
            {
                GameControl.Instance.GameOver();
                UnlockCursor();
            }
        }
    }
}