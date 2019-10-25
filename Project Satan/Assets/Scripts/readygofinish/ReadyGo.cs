using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadyGo : MonoBehaviour
{
    public bool ready;
    // Start is called before the first frame update
    void Awake()
    {
        Time.timeScale = 0;
        StartCoroutine(Go());
    }

    IEnumerator Go()
    {
        yield return new WaitForSecondsRealtime(1.6f);
        Time.timeScale = 1;
        ready = true;
    }
}
