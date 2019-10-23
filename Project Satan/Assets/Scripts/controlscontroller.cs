using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class controlscontroller : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
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
        
    }

    public void GetBack(InputAction.CallbackContext ctx)
    {
        SceneManager.LoadScene("Menu");
    }
}
