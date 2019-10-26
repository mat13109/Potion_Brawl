using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool oneisdead = false;
    CameraBehavior cameraBehavior;
    Animator menuPauseAnimator;

    ReadyGo readyGo;

    // Start is called before the first frame update
    void Start()
    {
        oneisdead = false;
        cameraBehavior = GameObject.Find("Main Camera").GetComponent<CameraBehavior>();
        menuPauseAnimator = GameObject.Find("PauseUI").GetComponent<Animator>();
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
        try
        {
            if (menuPauseAnimator.GetBool("paused") == true && readyGo.ready)
            {
                Time.timeScale = 0;
            }
            if (!menuPauseAnimator.GetBool("paused") == true && readyGo.ready)
            {
                Time.timeScale = 1;
            }
        } catch
        {

        }
        
    }

    public void OneIsDead()
    {
        oneisdead = true;
    }

    public void Shake()
    {
        cameraBehavior.StartTheShake();
    }
}
