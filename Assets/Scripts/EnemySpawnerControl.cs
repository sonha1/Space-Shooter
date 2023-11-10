using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerControl : MonoBehaviour
{
    public GameObject EnemyGO;
    public float maxSpawnRateInSeconds;
<<<<<<< HEAD
    public float numbers;
=======
>>>>>>> 3702de2b58f56314ab45d503fd9e25f19147b145
    public GameObject anEnemy;
    public float defaultMaxSpawnRateInSeconds;
    public GameObject Boss;

<<<<<<< HEAD
    public static int health;
=======
>>>>>>> 3702de2b58f56314ab45d503fd9e25f19147b145
    // Start is called before the first frame update
    void Start()
    {
        if (maxSpawnRateInSeconds == 0)
        {
            maxSpawnRateInSeconds = defaultMaxSpawnRateInSeconds;
        }

        Invoke("SpawnEnemy", maxSpawnRateInSeconds);
        //
<<<<<<< HEAD
        InvokeRepeating("IncreaseSpawnRate", 0f,15f);
=======
        InvokeRepeating("IncreaseSpawnRate", 0f, 20f);
>>>>>>> 3702de2b58f56314ab45d503fd9e25f19147b145
    }

    // Update is called once per frame
    void Update()
    {
    }

    void SpawnEnemy()
    {
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        GameObject anEnemy = (GameObject)Instantiate((EnemyGO));
        anEnemy.transform.position = new Vector2(Random.Range(min.x, max.x), max.y);

        ScheduleNextEnemySpawn();
    }

    void SpawnBoss()
    {
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
        anEnemy = (GameObject)Instantiate((Boss));
        anEnemy.transform.position = new Vector2(Random.Range(min.x, max.x), max.y);
    }


    public void ScheduleNextEnemySpawn()
    {
<<<<<<< HEAD

        maxSpawnRateInSeconds = numbers;
=======
>>>>>>> 3702de2b58f56314ab45d503fd9e25f19147b145
        
        float spawnInNSeconds;
        if (maxSpawnRateInSeconds > 0f)
        {
            spawnInNSeconds = Random.Range(1f, maxSpawnRateInSeconds);
            maxSpawnRateInSeconds--;
        }
        else
        {
            maxSpawnRateInSeconds = defaultMaxSpawnRateInSeconds;
            spawnInNSeconds = maxSpawnRateInSeconds;
        }

        Invoke("SpawnEnemy", spawnInNSeconds);
    }

    void IncreaseSpawnRate()
    {
        if (maxSpawnRateInSeconds > 1f)
        {
            maxSpawnRateInSeconds--;
        }

        if (maxSpawnRateInSeconds < 1f)
        {
            maxSpawnRateInSeconds = defaultMaxSpawnRateInSeconds;
        }
    }

    public void ScheduleEnemySpawner()
    {
        Invoke("SpawnEnemy", maxSpawnRateInSeconds);

        InvokeRepeating("IncreaseSpawnRate", 0f, 20f);
    }

    public void UnScheduleNextEnemySpawn()
    {
        CancelInvoke("SpawnEnemy");
        CancelInvoke("IncreaseSpawnRate");
    }

    public void ScheduleActiveBoss()
    {
        Invoke("SpawnBoss", 1f);
        Invoke("UnScheduleNextEnemySpawn", 0f);
    }

    public void unActiveBoss()
    {
        Destroy(anEnemy);
    }
}