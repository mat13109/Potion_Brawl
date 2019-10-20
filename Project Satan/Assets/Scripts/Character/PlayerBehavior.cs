using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBehavior : MonoBehaviour
{
    [SerializeField] float movSpeed;

    Rigidbody2D rb;
    Vector2 movementValues;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Interact()
    {
        Debug.Log("Interacted!");
    }

    public void GetMovementValues(InputAction.CallbackContext context)
    {
        movementValues = context.ReadValue<Vector2>();
    }

    private void Update()
    {
        rb.MovePosition(transform.position += new Vector3(movementValues.x, movementValues.y, 0) * Time.deltaTime * movSpeed);
    }
}
