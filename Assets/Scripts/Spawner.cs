using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject[] obstacles;
    private Alonso alonso;
    private int obstacleAmount = 1;
    private float minTime = 0.6f;
    private float maxTime = 1.8f;
    private float spawnRateMultiplier = 1f;

    // Start is called before the first frame update
    void Start()
    {
        alonso = FindObjectOfType<Alonso>();
        StartCoroutine(SpawnObstacle());        
    }

    public void IncreaseObstacleAmount()
    {
        obstacleAmount++;
    }

    public void IncreaseSpawnRate()
    {
        minTime = 0.6f;
        maxTime = 1f;
    }

    private IEnumerator SpawnObstacle()
    {
        while (true)
        {
            if (alonso != null && alonso.IsInvincible())
            {
                yield return null;
                continue;
            }

            int randomIndex = Random.Range(0, obstacles.Length);
            float randomTime = Random.Range(minTime, maxTime) * spawnRateMultiplier;
            Instantiate(obstacles[randomIndex], transform.position, Quaternion.identity);
            yield return new WaitForSeconds(randomTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(collision.gameObject);
    }
}
