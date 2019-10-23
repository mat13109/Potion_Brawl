using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombSpawn : MonoBehaviour
{

    public GameObject bombSpawn;
    public GameObject bombExplode;
    public GameObject bombGLue;
    public GameObject bombHumiliation;

    private float period = 0.0f;
    public float spawnTime = 1;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame


    void Update()
    {
        //shoot every spawnTime;
        if (period > spawnTime)
        {
            int randType = Random.Range(0, 100);

            Debug.Log(randType);

            if(randType < 50)
                Shoot(bombExplode);
             else if (randType >=  50 && randType < 85)
                Shoot(bombGLue);
            else
                Shoot(bombHumiliation);


            period = 0;
        }
        period += UnityEngine.Time.deltaTime;
    }

    void Shoot(GameObject bombType)
    {
        //instantiate bomb
        Instantiate(bombType, bombSpawn.transform.position, transform.rotation);

        //destroy after n sec
        //Destroy(bombType, 2.0f);
    }
}
