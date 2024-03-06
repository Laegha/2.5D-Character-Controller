using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerRotate : MonoBehaviour
{
    Rigidbody rb;
    PlayerInput playerInput;
    public float cameraSensitivity = 5f;
    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        rb = GetComponent<Rigidbody>();
        playerInput = GetComponent<PlayerInput>();
    }

    private void FixedUpdate()
    {
        float mouseX = playerInput.actions["Look"].ReadValue<Vector2>().x * cameraSensitivity;

        if (mouseX is not 0)
            rb.MoveRotation(rb.rotation * Quaternion.Euler(0f, mouseX, 0f));
    }
}
