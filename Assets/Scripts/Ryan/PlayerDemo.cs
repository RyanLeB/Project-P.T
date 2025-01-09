using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5.0f;
    public float mouseSensitivity = 2.0f;

    private float verticalRotation = 0.0f;
    private float verticalRotationLimit = 80.0f;

    public AudioSource footstepSource;
    
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // Mouse look
        float horizontalRotation = Input.GetAxis("Mouse X") * mouseSensitivity;
        transform.Rotate(0, horizontalRotation, 0);

        verticalRotation -= Input.GetAxis("Mouse Y") * mouseSensitivity;
        verticalRotation = Mathf.Clamp(verticalRotation, -verticalRotationLimit, verticalRotationLimit);
        Camera.main.transform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);

        // Movement
        float forwardMovement = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        float strafeMovement = Input.GetAxis("Horizontal") * speed * Time.deltaTime;

        Vector3 movement = new Vector3(strafeMovement, 0, forwardMovement);
        movement = transform.rotation * movement;

        transform.position += movement;
        
        if (movement.magnitude > 0 && !footstepSource.isPlaying)
        {
            footstepSource.Play();
        }
        else if (movement.magnitude == 0)
        {
            footstepSource.Stop();
        }
    }
}
