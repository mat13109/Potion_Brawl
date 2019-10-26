using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerBehavior : MonoBehaviour
{
    bool stunned;
    bool dead = false;

    GameManager gm;
    Animator pauseUIAnimator;
    [SerializeField] Animator playerAnimator;

    // The speed of the character movement
    [SerializeField] float movSpeed;
    [SerializeField] int team;
    [SerializeField] GameObject deathParticles;
    [SerializeField] float shootStrength;
    [SerializeField] float Stuck = 1.0f;

    [SerializeField] GameObject rumble;


    // Will contain the rigidbody of the character
    Rigidbody2D rb;
    // Will contain the WASD/left-stick axis' values
    Vector2 movementValues;
    // Last orientation
    Vector2 lastMovementValues = new Vector2(0, 0);

    AudioSource audioSourceKick;
    [SerializeField] AudioClip kick, menu,game;


    // Once at scene load
    private void Start()
    {
        // gets the rigidbody
        rb = GetComponent<Rigidbody2D>();
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        pauseUIAnimator = GameObject.Find("PauseUI").GetComponent<Animator>();

        audioSourceKick = GetComponent<AudioSource>();

    }

    public void SetStuck(float force)
    {
        Stuck = force;
        StartCoroutine(Unstuck(4f));
    }
    
    IEnumerator Unstuck(float duration)
    {
        yield return new WaitForSeconds(duration);
        if(Stuck > 1)
            SetStuck(1);
    }

    // Triggered when E/gamepad-south is pressed
    public void Interact(InputAction.CallbackContext context)
    {
        if (context.performed == true) // only on keydown
        {
            if (!stunned)
            {
                playerAnimator.SetTrigger("kick");
                Collider2D[] colliderShoot = Physics2D.OverlapCircleAll(transform.position, 1f);
                for (int i = 0; i < colliderShoot.Length; i++)
                {
                    if (colliderShoot[i].CompareTag("Bomb"))
                    {
                        colliderShoot[i].GetComponent<Rigidbody2D>().AddForce(lastMovementValues.normalized * shootStrength);
                        StartCoroutine(StretchBomb(colliderShoot[i].gameObject));
                        
                        audioSourceKick.clip = kick;
                        audioSourceKick.pitch = Random.Range(.9f, 1.1f);
                        audioSourceKick.volume = .3f;
                        audioSourceKick.Play();
                    }

                }
            }
                
        }
    }

    // Triggered when WASD/left-stick is used
    public void GetMovementValues(InputAction.CallbackContext context)
    {
        if (!stunned)
        {
            movementValues = context.ReadValue<Vector2>(); // store the value of the WASD/left-stick
            
        }
        else
        {
            movementValues = Vector2.zero;
        }
    }

    // Once per frame
    private void Update()
    {
        playerAnimator.SetBool("stunned", stunned);
        if (pauseUIAnimator.GetBool("paused"))
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }


        if (rb.velocity != Vector2.zero)
        {
            playerAnimator.SetFloat("h", rb.velocity.x);
            playerAnimator.SetFloat("v", rb.velocity.y);
            playerAnimator.SetFloat("ismoving", 1f);


        }
        else
        {
            playerAnimator.SetFloat("ismoving", 0f);
        }


        // moves the character with the rigidbody and prevents frame drops
        switch (stunned)
                {
                    case true:
                        //Player stunned => no control 
                        break;
                    case false:
                        rb.velocity = movementValues * movSpeed * Time.deltaTime / Stuck;
                        break;
                }

        // Moves on ice
        //rb.GetComponent<ConstantForce2D>().force = new Vector2(movementValues.x, movementValues.y) * Time.deltaTime * movSpeed;


        //save the movement
        if (movementValues != new Vector2(0, 0))
            lastMovementValues = movementValues;
        else
            if(team == 2)
                lastMovementValues = new Vector2(-1, 0);
            else
                lastMovementValues = new Vector2(1, 0);


        switch (team)
        {
            case 1:
                if (transform.position.x > 0)
                {
                    transform.position = new Vector2(0, transform.position.y);
                }
                if (stunned == false)
                {
                    if (transform.position.x < -8)
                    {
                        transform.position = new Vector2(-8, transform.position.y);
                    }
                    if (transform.position.y > 4)
                    {
                        transform.position = new Vector2(transform.position.x, 4);
                    }
                    if (transform.position.y < -4)
                    {
                        transform.position = new Vector2(transform.position.x, -4);
                    }
                    if (transform.position.x > -1)
                    {
                        transform.position = new Vector2(-1, transform.position.y);
                    }
                }
                break;
            case 2:
                if (transform.position.x < 0)
                {
                    transform.position = new Vector2(0, transform.position.y);
                }
                if (!stunned)
                {
                    if (transform.position.x > 8)
                    {
                        transform.position = new Vector2(8, transform.position.y);
                    }
                    if (transform.position.y > 4)
                    {
                        transform.position = new Vector2(transform.position.x, 4);
                    }
                    if (transform.position.y < -4)
                    {
                        transform.position = new Vector2(transform.position.x, -4);
                    }
                    if (transform.position.x < 1)
                    {
                        transform.position = new Vector2(1, transform.position.y);
                    }
                }

                break;
        }

    }

    public void KillYourself()
    {
        GameObject temp = Instantiate(deathParticles, transform.position, Quaternion.identity);
        Destroy(temp, 5);
        StopCoroutine("CooldownToGetUnstunned");
        stunned = true;
        dead = true;

        Instantiate(rumble);
        
        Invoke("LoadNewScene", 3);
        if (!gm.oneisdead)
        {
            gm.OneIsDead();
            gm.Shake();
            ScoreManager.RemoveOneLifeTo(team);
            if (PlayerPrefs.GetInt("P2") == 0 || PlayerPrefs.GetInt("P1") == 0)
            {
                audioSourceKick.clip = game;
                audioSourceKick.volume = .5f;
                audioSourceKick.Play();
            }
        }
        
        
        //Destroy(gameObject);
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Glue")
        {
            SetStuck(4.0f);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Glue")
        {
            SetStuck(1.0f);
        }
    }

    public void GetBackToStartMenu(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            pauseUIAnimator.SetBool("paused", !pauseUIAnimator.GetBool("paused"));
            
            audioSourceKick.clip = menu;
            audioSourceKick.volume = 1f;
            audioSourceKick.Play();
        }

    }

    public void QuitMenu()
    {
        if (pauseUIAnimator.GetBool("paused") == true)
            pauseUIAnimator.SetBool("paused", false);

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
        if(!dead)
            stunned = false;
        GetComponent<ConstantForce2D>().force = Vector2.zero;
    }

    void LoadNewScene()
    {
        SceneManager.LoadScene("FinalGameplay");
    }

    public void ExitTheGame(InputAction.CallbackContext context)
    {
        if (context.performed && GameObject.Find("PauseUI").GetComponent<Animator>().GetBool("paused"))
            SceneManager.LoadScene("Menu");
    }

    IEnumerator StretchBomb(GameObject colliderShoot)
    {
        float stretchSize = 0.3f;
        while(colliderShoot.transform.localScale.x > stretchSize)
        {
            colliderShoot.transform.localScale = new Vector3(colliderShoot.transform.localScale.x - 0.1f, 1, 1);
            yield return null;
        }

        while (colliderShoot.transform.localScale.x < 1)
        {
            colliderShoot.transform.localScale = new Vector3(colliderShoot.transform.localScale.x + 0.1f, 1, 1);
            yield return null;
        }
    }
}
