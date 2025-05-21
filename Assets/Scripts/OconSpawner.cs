using System.Collections;
using UnityEngine;

public class OconSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] obstacles;
    [SerializeField] private float obstacleSpeed = 15f;
    [SerializeField] private float initialDelay = 40f;
    [SerializeField] private GameObject warnOconPrefab;
    [SerializeField] private Transform warnOconSpawnPoint;

    private Alonso alonso;
    private GameObject currentWarnOcon;

    void Start()
    {
        alonso = FindObjectOfType<Alonso>();
        StartCoroutine(StartSpawningWithDelay());
    }

    private IEnumerator StartSpawningWithDelay()
    {
        yield return new WaitForSeconds(initialDelay);

        while (true)
        {
            if (alonso != null && alonso.IsInvincible())
            {
                yield return null;
                continue;
            }
            float waitTime = Random.Range(30f, 60f);
            if (warnOconPrefab != null)
            {
                yield return StartCoroutine(ShowWarnOcon(waitTime));
            } else {
                yield return new WaitForSeconds(waitTime - 3f);
            }
            SpawnObstacle();
            yield return null;
        }
    }

    private IEnumerator ShowWarnOcon(float waitTime)
    {
        float warnTime = 3f;
        yield return new WaitForSeconds(waitTime - warnTime);
        Vector3 spawnPos = warnOconSpawnPoint != null ? warnOconSpawnPoint.position : transform.position;
        currentWarnOcon = Instantiate(warnOconPrefab, spawnPos, Quaternion.identity);
        float flashDuration = warnTime;
        float flashInterval = 0.2f;
        SpriteRenderer sr = currentWarnOcon.GetComponent<SpriteRenderer>();
        float elapsed = 0f;
        while (elapsed < flashDuration)
        {
            if (sr != null)
                sr.enabled = !sr.enabled;
            yield return new WaitForSeconds(flashInterval);
            elapsed += flashInterval;
        }
        if (currentWarnOcon != null)
            Destroy(currentWarnOcon);
    }

    private void SpawnObstacle()
    {
        int randomIndex = Random.Range(0, obstacles.Length);
        GameObject obstacle = Instantiate(obstacles[randomIndex], transform.position, Quaternion.identity);

        Rigidbody2D rb = obstacle.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = Vector2.left * obstacleSpeed;
        }
        else
        {
            Debug.LogWarning("El obstáculo no tiene Rigidbody2D.");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(collision.gameObject);
    }
}
