using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetBackGround : MonoBehaviour
{
    public SpriteRenderer bg;

    public Sprite sprite1;
    public Sprite sprite2;

    private float min = 0f;
    private float max = 2f;
    private float random;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetRandomBackground()
    {
        random = Random.Range(min, max);
        if(random > 1)
        {
            bg.sprite = sprite1;
        }
        else
        {
            bg.sprite = sprite2;
        }
    }
}

