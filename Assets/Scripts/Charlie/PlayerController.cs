using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private float movementX;
    private float movementY;

    public enum MovementType
    {
        Walking,
        Running,
        Crawling
    }

    public MovementType movementType;

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
        Cursor.visible = false;
        playerCamera = GetComponentInChildren<Camera>();
    }

    public void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    public void OnLook(InputValue lookValue)
    {
        float lookX = lookValue.Get<Vector2>()[0] * sensitivity;
        float lookY = lookValue.Get<Vector2>()[1] * sensitivity;

        cameraVerticalRotation -= lookY;
        cameraVerticalRotation = Mathf.Clamp(cameraVerticalRotation, -cameraVerticalRotationLimit, cameraVerticalRotationLimit);

        this.transform.Rotate(0, lookX, 0);
        playerCamera.transform.localRotation = Quaternion.Euler(cameraVerticalRotation, 0, 0);
    }

    void FixedUpdate()
    {
        CheckGround();
        HandleMovmentStates();
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);

        if (!isGrounded)
        {
            movement.y = gravity;
        }
        if (isGrounded)
        {
            movement.y = 0;
        }

        gameObject.transform.Translate(movement * Time.fixedDeltaTime * speed);
    }

    public void HandleMovmentStates()
    {
        switch (movementType)
        {
            case MovementType.Walking:
                speed = 5f;
                break;
            case MovementType.Running:
                speed = 10f;
                break;
            case MovementType.Crawling:
                speed = 2f;
                break;
        }
    }

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
