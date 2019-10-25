using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class winsound : MonoBehaviour
{
    [SerializeField] AudioClip audioClip;
    [SerializeField] AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(yes());
    }

    // Update is called once per frame
    IEnumerator yes()
    {
        yield return new WaitForSecondsRealtime(3);
        audioSource.clip = audioClip;
        audioSource.Stop();
        audioSource.Play();
    }
}
