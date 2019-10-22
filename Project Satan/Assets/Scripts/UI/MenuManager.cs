using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class MenuManager : MonoBehaviour
{
    static public int index;
    [SerializeField] int numberOfButtons;

    // Start is called before the first frame update
    void Start()
    {
        index = 0;
        
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GoUpMenu(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            if (index != 0)
            {
                index-=3;
            }
        }

    }

    public void GoDownMenu(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            if (index != numberOfButtons - 1)
            {
                index+=3;
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
                    SceneManager.LoadScene("FinalGameplay");
                    break;
                case 3:// Quit
#if UNITY_EDITOR
                    UnityEditor.EditorApplication.isPlaying = false;
#else
                Application.Quit();
#endif
                    break;
            }
        }
        
    }
}
