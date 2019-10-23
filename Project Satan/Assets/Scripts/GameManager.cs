using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool oneisdead = false;
    CameraBehavior cameraBehavior;
    // Start is called before the first frame update
    void Start()
    {
        oneisdead = false;
        cameraBehavior = GameObject.Find("Main Camera").GetComponent<CameraBehavior>();
    }

    // Update is called once per frame
    void Update()
    {
        
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
