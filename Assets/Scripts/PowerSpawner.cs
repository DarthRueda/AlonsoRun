using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] powerUps;
    private float powerUpSpawnInterval = 30f;

    void Start()
    {
        StartCoroutine(SpawnPowerUp());
    }

    private IEnumerator SpawnPowerUp()
    {
        while (true)
        {
            yield return new WaitForSeconds(powerUpSpawnInterval);
            List<GameObject> filteredPowerUps = new List<GameObject>();

            foreach (var powerUp in powerUps)
            {
                if (powerUp.CompareTag("Invencible") || powerUp.CompareTag("DobleSalto"))
                {
                    filteredPowerUps.Add(powerUp);
                }
            }

            if (filteredPowerUps.Count > 0)
            {
                int randomIndex = Random.Range(0, filteredPowerUps.Count);
                Instantiate(filteredPowerUps[randomIndex], transform.position, Quaternion.identity);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(collision.gameObject);
    }
}
