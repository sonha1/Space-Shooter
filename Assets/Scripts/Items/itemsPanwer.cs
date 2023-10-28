using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemsPanwer : MonoBehaviour
{
    public GameObject itemResurrection;
    public GameObject ItemsCannon;
    public GameObject ItemsKamikaze;

    public float RateResurrection;
    public float RateCannon;
    public float RateKamikaze;

    public float maxSpawnRateInSeconds = 5f;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("SpawnCannon", 20f);
        Invoke("SpawnResurrection", 35f);
        Invoke("SpawnKamikaze", 45f);

        InvokeRepeating("IncreaseSpawnRate", 0f, 5f);

    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void SpawnResurrection()
    {
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
        if (itemResurrection)
        {
            GameObject anResurrection = (GameObject)Instantiate((itemResurrection));
            anResurrection.transform.position = new Vector2(Random.Range(min.x, max.x), max.y);
        }

        ScheduleNextResurrectionSpawn();
    }

    void SpawnCannon()
    {
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        if(ItemsCannon){
            GameObject anCannon = (GameObject)Instantiate((ItemsCannon));
            anCannon.transform.position = new Vector2(Random.Range(min.x, max.x), max.y);
        }

        ScheduleNextCannonSpawn();
    }

    void SpawnKamikaze()
    {
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        if(ItemsKamikaze){
            GameObject anKamikaze = (GameObject)Instantiate((ItemsKamikaze));
            anKamikaze.transform.position = new Vector2(Random.Range(min.x, max.x), max.y);
        }

        ScheduleNextKamikazeSpawn();
    }
    public void ScheduleNextResurrectionSpawn()
    {

        maxSpawnRateInSeconds = RateResurrection;

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

        maxSpawnRateInSeconds = RateKamikaze;

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

    public void ScheduleNextCannonSpawn()
    {

        maxSpawnRateInSeconds = RateCannon;

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
        Invoke("SpawnCannon", spawnInNSeconds);
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



}
