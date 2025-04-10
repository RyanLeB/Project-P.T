using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using static GlobalVariables;

public class PlayerController : MonoBehaviour
{

    //Movement
    private float movementX;
    private float movementY;

    public CharacterController characterController;
    public Lighter lighter;

    public bool cameraLocked = false;
    public bool movementLocked = false;

    bool isCrouching = false;
    bool isRunning = false;
    bool isOnStairs = false;

    public enum MovementType
    {
        Walking,
        Running,
        Crawling,
        Stairs
    }

    public MovementType movementType;

    private Vector3 velocity;

    [SerializeField] private float currentSpeed;

    //Camera
    private float cameraVerticalRotation = 0f;

    public Camera playerCamera;

    //GravityETC
    private bool isGrounded;

    
    //Footsteps
    public AudioClip[] footstepSounds;
    private AudioSource audioSource;
    private Coroutine footstepCoroutine;
    
    

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        playerCamera = GetComponentInChildren<Camera>();
        audioSource = GetComponent<AudioSource>();
    }

    void FixedUpdate()
    {
        CheckGround();
        HandleMovementStates();
        HandleMovement();
        #if UNITY_EDITOR
        ChangeSpeed(5f); // Changes speeds of the player pressing 1, 2 and pressing 3 is normal speed
        Loop10Teleport();
        LighterCheat(); // Activates the lighter when pressing L and deactivates it when pressing L again
        #endif
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
        
        if (movementX != 0 || movementY != 0)
        {
            if (footstepCoroutine == null)
            {
                footstepCoroutine = StartCoroutine(PlayFootstepSounds());
            }
        }
        else
        {
            if (footstepCoroutine != null)
            {
                StopCoroutine(footstepCoroutine);
                footstepCoroutine = null;
            }
        }
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

        float lookX = lookValue.Get<Vector2>()[0] * cameraSensitivity;
        float lookY = lookValue.Get<Vector2>()[1] * cameraSensitivity;

        cameraVerticalRotation -= lookY;
        cameraVerticalRotation = Mathf.Clamp(cameraVerticalRotation, -cameraVerticalRotationLimit, cameraVerticalRotationLimit);

        this.transform.Rotate(0, lookX, 0);
        playerCamera.transform.localRotation = Quaternion.Euler(cameraVerticalRotation, 0, 0);
    }

    public void OnPause()
    {
        GameManager.manager.PauseGame();
    }


    /// <summary>
    /// Handles the player's movement.
    /// </summary>
    private void HandleMovement()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        movement = transform.TransformDirection(movement);

        if (velocity.y < 0f && isGrounded)
        {
            velocity.y = -2.0f;
        }

        characterController.Move(velocity * Time.deltaTime);
        velocity.y += gravity * Time.deltaTime;

        UpdateMovementType();

        characterController.Move(movement * Time.fixedDeltaTime * currentSpeed);
    }

    /// <summary>
    /// This is mostly a debug function, this will not be needed really after a point.
    /// </summary>
    private void UpdateMovementType()
    {
        if (Input.GetKey(KeyCode.C))
        {
            isCrouching = true;
            movementType = MovementType.Crawling;
        }
        //if (Input.GetKey(KeyCode.LeftShift))
        //{
        //    isRunning = true;
        //    movementType = MovementType.Running;
        //}
        if (!isRunning && !isCrouching && !isOnStairs)
        {
            movementType = MovementType.Walking;
        }
        else
        {
            isCrouching = false;
            isRunning = false;
        }
    }

    /// <summary>
    /// Handles the player's movement states based on the type of movement the player is performing.
    /// </summary>
    public void HandleMovementStates()
    {
        switch (movementType)
        {
            case MovementType.Walking:
                currentSpeed = defaultSpeed;
                characterController.height = defaultHeight;
                break;
            case MovementType.Running:
                currentSpeed = defaultSpeed * runningSpeedMultiplier;
                characterController.height = defaultHeight;
                break;
            case MovementType.Crawling:
                currentSpeed = defaultSpeed * crouchingSpeedMultiplier;
                characterController.height =  defaultHeight * crouchingHeightMultiplier;
                break;
            case MovementType.Stairs:
                currentSpeed = defaultSpeed * stairsSpeedMultiplier;
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

    public void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("Test");

        if (collision.gameObject.CompareTag("Stairs"))
        {
            Debug.Log(collision.gameObject.name);
            isOnStairs = true;
            movementType = MovementType.Stairs;
        }
    }

    public void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Stairs"))
        {
            isOnStairs = false;
        }
    }
    
    private IEnumerator PlayFootstepSounds()
    {
        // Initial delay before playing the first footstep sound
        yield return new WaitForSeconds(0.2f);

        while (true)
        {
            if (footstepSounds.Length > 0)
            {
                audioSource.clip = footstepSounds[UnityEngine.Random.Range(0, footstepSounds.Length)];
                audioSource.Play();
            }
            yield return new WaitForSeconds(.6f);
        }
    }
    
    #if UNITY_EDITOR
    private void ChangeSpeed(float speed)
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            defaultSpeed = speed;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            defaultSpeed = speed * 2;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            defaultSpeed = 1.5f;
        }
    }

    // ----------------------------------------------------
    // Gives Player the lighter
    // ----------------------------------------------------
    private void LighterCheat()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            if (!lighter.hasLighter)
            {
                lighter.hasLighter = true;
                lighter.gameObject.SetActive(true);
            }
            else
            {
                lighter.hasLighter = false;
                lighter.gameObject.SetActive(false);
            }
        }
    }
    
    private void Loop10Teleport()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            transform.position = new Vector3(9.5558939f,-98.0299988f,15.7399998f);
        }
    }
    #endif 
}
