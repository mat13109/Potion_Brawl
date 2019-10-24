using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCannon : MonoBehaviour
{

    public float speedRotate = 5.0f;

    private float switchTime;
    private float min = 0;
    private float max = 100;

    private float TimeToRefresh = 3f;

    private float period;



    // Start is called before the first frame update
    void Start()
    {
        switchRotateCannon();
    }

    // Update is called once per frame
    void Update()
    {
        if (switchTime < 50)
        {
            transform.Rotate(Vector3.back * speedRotate * Time.deltaTime);
            //Debug.Log("Switch back" + switchTime);
        }
        else
        {
            transform.Rotate(Vector3.forward * speedRotate * Time.deltaTime);
            //Debug.Log("Switch back" + switchTime);
        }

        if (period > TimeToRefresh)
        {
            switchRotateCannon();
            period = 0;
        }
        period += UnityEngine.Time.deltaTime;


        // TODO Switch cannon rotation
        //

    }

    void switchRotateCannon()
    {
      switchTime = Random.Range(min, max);
      
    }
}
