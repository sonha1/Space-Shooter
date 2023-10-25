using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemsPanwerGO : MonoBehaviour
{

    public GameObject AsteroidGO01;
  

    public GameObject itemResurrection;
    public GameObject ItemsCannon;
    public GameObject ItemsKamikaze;
    public float maxSpawnRateInSeconds = 5f;
    // public GameObject anEnemy;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("SpawnAsteroid", 10f);
        Invoke("SpawnResurrection", 25f);
        Invoke("SpawnKamikaze", 15f);
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

    void SpawnResurrection()
    {
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        GameObject anResurrection = (GameObject)Instantiate((itemResurrection));
        anResurrection.transform.position = new Vector2(Random.Range(min.x, max.x), max.y);

        ScheduleNextResurrectionSpawn();
    }

    void SpawnCannon()
    {
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        GameObject anCannon = (GameObject)Instantiate((ItemsCannon));
        anCannon.transform.position = new Vector2(Random.Range(min.x, max.x), max.y);

        ScheduleNextAsteroidSpawn();
    }

    void SpawnKamikaze()
    {
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        GameObject anKamikaze = (GameObject)Instantiate((ItemsKamikaze));
        anKamikaze.transform.position = new Vector2(Random.Range(min.x, max.x), max.y);

        ScheduleNextKamikazeSpawn();
    }

    public void ScheduleNextAsteroidSpawn()
    {

        maxSpawnRateInSeconds = 10f;

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

    public void ScheduleNextResurrectionSpawn()
    {

        maxSpawnRateInSeconds = 15f;

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
        Invoke("SpawnResurrection", spawnInNSeconds);
    }


    public void ScheduleNextKamikazeSpawn()
    {

        maxSpawnRateInSeconds = 5f;

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
        Invoke("SpawnKamikaze", spawnInNSeconds);
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
