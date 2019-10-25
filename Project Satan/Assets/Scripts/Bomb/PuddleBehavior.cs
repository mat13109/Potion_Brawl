using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuddleBehavior : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<AudioSource>().Play();
        Destroy(gameObject, 1.5f);
    }
}
