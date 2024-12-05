using System;
using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;
public class PlayerControl : MonoBehaviour
{
    [SerializeField] public float playerHealth;
    [SerializeField] private float currentSpeed;
    [SerializeField] private float energy;
    [SerializeField] private float cameraSensitivity;
    [SerializeField] private Camera playerCamera;
    [SerializeField] private Light playerLight;
    [SerializeField] private AudioSource _audioSource;
    private float currentYRotation = 0f;
    private float currentXRotation = 0f;

    private float normalSpeed = 6f;
    private float runningSpeed = 10f;

    private float xDirection;
    private float zDirection;
    private Rigidbody _rigidbody;
    public bool isHiding = false;
    //private bool isRunning = false;
    private RaycastHit hit;

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
        bool playerIsMoving = movement.x != 0 || movement.z != 0;
        if (playerIsMoving && !_audioSource.isPlaying)
        {
            _audioSource.Play();
        }
        else if (!playerIsMoving && _audioSource.isPlaying) 
        {
            _audioSource.Stop();
        }
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
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, 15f))
        {
            ItemControl interactable = hit.collider.GetComponent<ItemControl>();

            if (interactable != null)
            {                
                interactable.Interact();
                Game_Manager.Instance.score += 100;
            }
        }
    }
    private void InteractWithDoor()
    {
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, 10f))
        {
            if (hit.collider.CompareTag("Door"))
            {
                hit.collider.gameObject.transform.transform.DOMove(hit.collider.gameObject.transform.transform.position + new Vector3(-8f, 0f, 8f), 1f);
                hit.collider.gameObject.transform.DORotate(new Vector3(0f, 90f, 0f), 1f); 
            }
        }
    }
    private void Hide()
    {
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, 10f))
        {
            if (hit.collider.CompareTag("HidingArea"))
            {
                transform.position = hit.collider.transform.position;
                transform.rotation = hit.collider.transform.rotation;
                currentSpeed = 0f;
                isHiding = true;
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
            InteractWithDoor();
            if (isHiding == false)
            {
                Hide();
            }
            else
            {
                currentSpeed = normalSpeed;
                isHiding = false;
            }
        }
    }
    public void ReadRunButton(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (isHiding == false)
            {
                currentSpeed = runningSpeed;
                //isRunning = true;
            }
        }
        else
        {
            if (isHiding == false)
            {
                currentSpeed = normalSpeed;
                //isRunning = false;
            }
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
            playerHealth = playerHealth - 1f;
            if (playerHealth <= 0)
            {
                Game_Manager.Instance.GameOver();
                UnlockCursor();
            }
        }
    }
}