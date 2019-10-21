using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerBehavior : MonoBehaviour
{
    bool stunned;
    // The speed of the character movement
    [SerializeField] float movSpeed;
    [SerializeField] int team;
    [SerializeField] GameObject deathParticles;
    [SerializeField] float shootStrength;


    // Will contain the rigidbody of the character
    Rigidbody2D rb;
    // Will contain the WASD/left-stick axis' values
    Vector2 movementValues;
    // Last orientation
    Vector2 lastMovementValues = new Vector2(0, 0);


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
        {
            Debug.Log("Interacted!");
            Collider2D[] colliderShoot = Physics2D.OverlapCircleAll(transform.position, 1f);
            for (int i = 0; i < colliderShoot.Length; i++)
            {
                if (colliderShoot[i].CompareTag("Bomb"))
                    colliderShoot[i].GetComponent<Rigidbody2D>().AddForce(lastMovementValues * shootStrength);
            }
        }
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
        //rb.MovePosition(transform.position += new Vector3(movementValues.x, movementValues.y, 0) * Time.deltaTime * movSpeed);
        if (!stunned)
            rb.GetComponent<ConstantForce2D>().force = new Vector2(movementValues.x, movementValues.y) * Time.deltaTime * movSpeed;

        //save the movement
        if (movementValues != new Vector2(0, 0))
            lastMovementValues = movementValues;
        

        switch (team)
        {
            case 1:
                if (transform.position.x > 0)
                {
                    transform.position = new Vector2(0, transform.position.y);
                }
                break;
            case 2:
                if (transform.position.x < 0)
                {
                    transform.position = new Vector2(0, transform.position.y);
                }
                break;
        }
    }

    public void KillYourself()
    {
        GameObject temp = Instantiate(deathParticles, transform.position, Quaternion.identity);
        Destroy(temp, 5);
        stunned = true;
        Invoke("LoadNewScene", 5);
    }

    public void GetStunnned(float i)
    {
        stunned = true;
        StartCoroutine(CooldownToGetUnstunned(i));
    }

    IEnumerator CooldownToGetUnstunned(float i)
    {
        StopCoroutine("CooldownToGetUnstunned");
        yield return new WaitForSeconds(i);
        stunned = false;
        GetComponent<ConstantForce2D>().force = Vector2.zero;
    }

    void LoadNewScene()
    {
        SceneManager.LoadScene(0);
    }
}
