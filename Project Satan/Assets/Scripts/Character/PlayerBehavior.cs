using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBehavior : MonoBehaviour
{
    // The speed of the character movement
    [SerializeField] float movSpeed;

    // Will contain the rigidbody of the character
    Rigidbody2D rb;
    // Will contain the WASD/left-stick axis' values
    Vector2 movementValues;

    // Once at scene load
    private void Start()
    {
        // gets the rigidbody
        rb = GetComponent<Rigidbody2D>();
    }

    // Triggered when E/gamepad-south is pressed
    public void Interact(InputAction.CallbackContext context)
    {
        if (context.performed == true) // only on keydown
            Debug.Log("Interacted!");
    }

    // Triggered when WASD/left-stick is used
    public void GetMovementValues(InputAction.CallbackContext context)
    {
        movementValues = context.ReadValue<Vector2>(); // store the value of the WASD/left-stick
    }

    // Once per frame
    private void Update()
    {
        // moves the character with the rigidbody and prevents frame drops
        rb.MovePosition(transform.position += new Vector3(movementValues.x, movementValues.y, 0) * Time.deltaTime * movSpeed);
    }
}
