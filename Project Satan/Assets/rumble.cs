using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class rumble : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        foreach (Gamepad gamepad in Gamepad.all)
        {
            gamepad.SetMotorSpeeds(1, 1);
        }
        StartCoroutine(KillYourself());
    }

    IEnumerator KillYourself()
    {
        yield return new WaitForSecondsRealtime(.2f);
        foreach (Gamepad gamepad in Gamepad.all)
        {
            gamepad.SetMotorSpeeds(0, 0);
        }
        Destroy(gameObject);
    }
}
