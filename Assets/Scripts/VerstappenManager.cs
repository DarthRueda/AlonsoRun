using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerstappenManager : MonoBehaviour
{
    [SerializeField] private GameObject verstappenPrefab;
    [SerializeField] private Transform verstappenSpawner;
    private GameObject spawnedVerstappen;

    private int verstappenSpawnScore = -1;
    private int verstappenLastMoveScore = -1;
    private float verstappenMoveStep = 5.0f; // 100px if 1 unit = 100px

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnedVerstappen != null && GameManager.Instance != null)
        {
            int currentScore = GameManager.Instance.GetCurrentScore();
            if (currentScore >= verstappenLastMoveScore + 50)
            {
                spawnedVerstappen.transform.position += new Vector3(-verstappenMoveStep, 0, 0);
                verstappenLastMoveScore += 50;
            }
        }
    }

    public void SpawnVerstappen()
    {
        if (verstappenPrefab != null && verstappenSpawner != null && spawnedVerstappen == null)
        {
            spawnedVerstappen = Instantiate(verstappenPrefab, verstappenSpawner.position, Quaternion.identity);
            verstappenSpawnScore = GameManager.Instance != null ? GameManager.Instance.GetCurrentScore() : 0;
            verstappenLastMoveScore = verstappenSpawnScore;
        }
    }
}
