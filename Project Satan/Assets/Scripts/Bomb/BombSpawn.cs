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
    private float spawnTime;
    private float minTime = 0.5f;
    private float maxTime = 3f;



    // Start is called before the first frame update
    void Start()
    {
        SetRandomSpawntime();
        Debug.Log("First bomb" + spawnTime);
    }

    // Update is called once per frame


    void Update()
    {
        //shoot every spawnTime;
        if (period > spawnTime)
        {
            SetRandomSpawntime();
            int randType = Random.Range(0, 100);

            //Debug.Log(randType);

            if(randType < 75)
                Shoot(bombExplode);
             else if (randType >=  75 && randType < 90)
                Shoot(bombGLue);
            else
                Shoot(bombHumiliation);


            period = 0;
        }
        period += UnityEngine.Time.deltaTime;
    }

    void Shoot(GameObject bombType)
    {

        //spawnTime = Random.Range(minTime, maxTime);
        //instantiate bomb
        Instantiate(bombType, bombSpawn.transform.position, transform.rotation);

        //destroy after n sec
        //Destroy(bombType, 2.0f);
    }

    void SetRandomSpawntime()
    {
        spawnTime = Random.Range(minTime, maxTime);
        Debug.Log("Next bomb spawn in " + spawnTime + " seconds.");
    }
}
