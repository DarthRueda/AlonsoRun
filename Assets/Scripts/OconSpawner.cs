using System.Collections;
using UnityEngine;

public class OconSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] obstacles;
    [SerializeField] private float obstacleSpeed = 20f;
    [SerializeField] private float initialDelay = 10f;

    void Start()
    {
        StartCoroutine(StartSpawningWithDelay());
    }

    private IEnumerator StartSpawningWithDelay()
    {
        yield return new WaitForSeconds(initialDelay);

        while (true)
        {
            SpawnObstacle();

            float waitTime = Random.Range(30f, 60f);
            yield return new WaitForSeconds(waitTime);
        }
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
