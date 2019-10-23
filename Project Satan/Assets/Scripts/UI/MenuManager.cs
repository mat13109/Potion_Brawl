using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class MenuManager : MonoBehaviour
{
    static public int index;
    [SerializeField] int numberOfButtons;
    Animator warningAnim;

    // Start is called before the first frame update
    void Start()
    {
        index = 0;
        
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        warningAnim = GameObject.Find("Warning").GetComponent<Animator>();
    }

    private void Awake()
    {
        InputSystem.onDeviceChange +=
        (device, change) =>
        {
            switch (change)
            {
                case InputDeviceChange.Added:
                    SceneManager.LoadScene("Menu");
                    break;
                case InputDeviceChange.Disconnected:
                    SceneManager.LoadScene("Menu");
                    break;
                case InputDeviceChange.Reconnected:
                    SceneManager.LoadScene("Menu");
                    break;
                case InputDeviceChange.Removed:
                    SceneManager.LoadScene("Menu");
                    break;
                default:
                    // See InputDeviceChange reference for other event types.
                    break;
            }
        };
    }

    // Update is called once per frame
    void Update()
    {
        var allGamepads = Gamepad.all;
        if (allGamepads.Count < 2)
        {
            warningAnim.SetBool("warning", true);
            Destroy(GameObject.Find("UIController"));
            Destroy(GameObject.Find("UIController (1)"));
        } else
        {
            warningAnim.SetBool("warning", false);
        }

        
    }



    public void GoUpMenu(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            if (index != 0)
            {
                index--;
            }
        }

    }

    public void GoDownMenu(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            if (index != numberOfButtons - 1)
            {
                index++;
            }
        }

    }

    public void Submit(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            switch (index)
            {
                case 0: // Play
                    ScoreManager.ResetScores();
                    SceneManager.LoadScene("FinalGameplay");
                    break;
                case 3:// Quit
#if UNITY_EDITOR
                    UnityEditor.EditorApplication.isPlaying = false;
#else
                Application.Quit();
#endif
                    break;
                case 1://controls
                    SceneManager.LoadScene("Controls");
                    break;
            }
        }
        
    }
}
