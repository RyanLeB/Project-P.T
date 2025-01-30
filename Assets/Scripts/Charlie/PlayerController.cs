using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private float movementX;
    private float movementY;

    public CharacterController characterController;

    public bool cameraLocked = false;
    public bool movementLocked = false;

    bool isCrouching = false;
    bool isRunning = false;

    public enum MovementType
    {
        Walking,
        Running,
        Crawling
    }

    public MovementType movementType;

    private Vector3 velocity;

    [SerializeField] private float speed;
    private float sensitivity = 0.15f;

    private float cameraVerticalRotationLimit = 65f;

    private float cameraVerticalRotation = 0f;

    private Camera playerCamera;

    public float distance = 1.05f;
    private bool isGrounded;
    [SerializeField] private float gravity = -2;


    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        playerCamera = GetComponentInChildren<Camera>();
    }

    /// <summary>
    /// Gets called when the player moves using the movement keys.
    /// </summary>
    /// <param name="movementValue">Input value used to represent and manage input data related to movement controls</param>
    public void OnMove(InputValue movementValue)
    {
        if (movementLocked)
        {
            return;
        }

        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    /// <summary>
    /// Gets called when the player looks around using the mouse.
    /// </summary>
    /// <param name="lookValue">Input value used to represent and manage input data related to camera controls</param>
    public void OnLook(InputValue lookValue)
    {
        if (cameraLocked)
        {
            return;
        }

        float lookX = lookValue.Get<Vector2>()[0] * sensitivity;
        float lookY = lookValue.Get<Vector2>()[1] * sensitivity;

        cameraVerticalRotation -= lookY;
        cameraVerticalRotation = Mathf.Clamp(cameraVerticalRotation, -cameraVerticalRotationLimit, cameraVerticalRotationLimit);

        this.transform.Rotate(0, lookX, 0);
        playerCamera.transform.localRotation = Quaternion.Euler(cameraVerticalRotation, 0, 0);
    }

    public void OnPause()
    {
        GameManager.manager.PauseGame();
    }

    void FixedUpdate()
    {
        CheckGround();
        HandleMovmentStates();
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);

        if (velocity.y < 0f && isGrounded)
        {
            velocity.y = -2.0f;
        }

        characterController.Move(velocity * Time.deltaTime);
        velocity.y += gravity * Time.deltaTime;

        if (Input.GetKey(KeyCode.C))
        {
            isCrouching = true;
            movementType = MovementType.Crawling;
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            isRunning = true;
            movementType = MovementType.Running;
        }
        if (!isRunning && !isCrouching)
        {
            movementType = MovementType.Walking;
        }
        else
        {
            isCrouching = false;
            isRunning = false;
        }

        gameObject.transform.Translate(movement * Time.fixedDeltaTime * speed);
    }

    /// <summary>
    /// Handles the player's movement states based on the type of movement the player is performing.
    /// </summary>
    public void HandleMovmentStates()
    {
        switch (movementType)
        {
            case MovementType.Walking:
                speed = 5f;
                characterController.height = 2f;
                break;
            case MovementType.Running:
                speed = 10f;
                characterController.height = 2f;
                break;
            case MovementType.Crawling:
                speed = 2f;
                characterController.height = 0.5f;
                break;
        }
    }

    /// <summary>
    /// Checks if the player is grounded.
    /// </summary>
    private void CheckGround()
    {
        Debug.DrawRay(transform.position, Vector3.down * distance, Color.red);

        if (Physics.Raycast((transform.position), Vector3.down * distance, out RaycastHit hit, distance))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }
}
