using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;


public class Victory : MonoBehaviour
{
    [SerializeField] Text text;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("P1") > PlayerPrefs.GetInt("P2"))
        {
            text.text = "Wouah, c'est le joueur 1 qui a gagné !";
        } else
        {
            text.text = "Wouah, c'est le joueur 2 qui a gagné !";
        }
    }

    // Update is called once per frame
    void Update()
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
}
