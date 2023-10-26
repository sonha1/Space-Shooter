using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawnerControl : MonoBehaviour
{

    public GameObject AsteroidGO01;
    public GameObject AsteroidGO02;
    public GameObject AsteroidGO03;

    public float RateAsteroid01;
    public float RateAsteroid02;
    public float RateAsteroid03;
    public float maxSpawnRateInSeconds = 5f;
   // public GameObject anEnemy;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("SpawnAsteroid", 10f);
        Invoke("SpawnAsteroid02", 20f);
        Invoke("SpawnAsteroid03", 30f);
        //
        InvokeRepeating("IncreaseSpawnRate", 0f, 5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnAsteroid()
    {
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        GameObject anAsteroid = (GameObject)Instantiate((AsteroidGO01));
        anAsteroid.transform.position = new Vector2(Random.Range(min.x, max.x), max.y);

        ScheduleNextAsteroidSpawn();
    }

    void SpawnAsteroid02()
    {
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        GameObject anAsteroid = (GameObject)Instantiate((AsteroidGO02));
        anAsteroid.transform.position = new Vector2(Random.Range(min.x, max.x), max.y);

        ScheduleNextAsteroidSpawn02();
    }

    void SpawnAsteroid03()
    {
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        GameObject anAsteroid = (GameObject)Instantiate((AsteroidGO03));
        anAsteroid.transform.position = new Vector2(Random.Range(min.x, max.x), max.y);

        ScheduleNextAsteroidSpawn03();
    }


    public void ScheduleNextAsteroidSpawn()
    {

        maxSpawnRateInSeconds = RateAsteroid01;

        float spawnInNSeconds;
        if (maxSpawnRateInSeconds > 1f)
        {
            spawnInNSeconds = Random.Range(1f, maxSpawnRateInSeconds);
            maxSpawnRateInSeconds--;
        }
        else
        {
            spawnInNSeconds = 1f;
        }
        Invoke("SpawnAsteroid", spawnInNSeconds);
    }

    public void ScheduleNextAsteroidSpawn02()
    {

        maxSpawnRateInSeconds = RateAsteroid01;

        float spawnInNSeconds;
        if (maxSpawnRateInSeconds > 1f)
        {
            spawnInNSeconds = Random.Range(1f, maxSpawnRateInSeconds);
            maxSpawnRateInSeconds--;
        }
        else
        {
            spawnInNSeconds = 1f;
        }
        Invoke("SpawnAsteroid02", spawnInNSeconds);
    }

    public void ScheduleNextAsteroidSpawn03()
    {

        maxSpawnRateInSeconds = RateAsteroid01;

        float spawnInNSeconds;
        if (maxSpawnRateInSeconds > 1f)
        {
            spawnInNSeconds = Random.Range(1f, maxSpawnRateInSeconds);
            maxSpawnRateInSeconds--;
        }
        else
        {
            spawnInNSeconds = 1f;
        }
        Invoke("SpawnAsteroid03", spawnInNSeconds);
    }

    void IncreaseSpawnRate()
    {
        if (maxSpawnRateInSeconds > 1f)
        {
            maxSpawnRateInSeconds--;
        }

        if (maxSpawnRateInSeconds == 1f)
        {
            CancelInvoke(("IncreaseSpawnRate"));
        }

    }


    public void UnScheduleNextAsteroidSpawn()
    {
        CancelInvoke("SpawnAsteroid");
        CancelInvoke("IncreaseSpawnRate");
    }
}
