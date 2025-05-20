using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FondoRepeat : MonoBehaviour
{
    [SerializeField] private GameObject[] fondoPrefabs;
    [SerializeField] private GameObject invincibleFondoPrefab;
    private float spriteWidth;
    private Alonso alonso;
    private bool isHistoriaScene;

    void Start()
    {
        alonso = FindObjectOfType<Alonso>();
        BoxCollider2D groundCollider = GetComponent<BoxCollider2D>();
        spriteWidth = groundCollider.size.x;
        isHistoriaScene = SceneManager.GetActiveScene().name == "Historia";
    }

    void Update()
    {
        if (transform.position.x < -spriteWidth)
        {
            ReplaceFondo();
        }
    }

    private void ReplaceFondo()
    {
        transform.position = new Vector2(transform.position.x + 2f * spriteWidth, transform.position.y);

        SpriteRenderer currentRenderer = GetComponent<SpriteRenderer>();
        SpriteRenderer newRenderer = null;

        if (isHistoriaScene)
        {
            int score = GameManager.Instance != null ? GameManager.Instance.GetCurrentScore() : 0;
            string[] fondoNames = { "MINARDI", "RENAULT", "MCLAREN", "FERRARI", "ALPINE", "ASTON" };
            int fondoStage = Mathf.Min(score / 333, fondoNames.Length - 1);
            string currentFondoName = fondoNames[fondoStage];

            // Si puntos >= 2000, buscar fondo que contenga REDBULL
            if (score >= 2000)
            {
                List<GameObject> redbullFondos = new List<GameObject>();
                foreach (var fondo in fondoPrefabs)
                {
                    if (fondo != null && fondo.name.ToUpper().Contains("REDBULL"))
                    {
                        redbullFondos.Add(fondo);
                    }
                }
                if (redbullFondos.Count > 0)
                {
                    int idx = Random.Range(0, redbullFondos.Count);
                    newRenderer = redbullFondos[idx].GetComponent<SpriteRenderer>();
                }
            }
            else if (alonso != null && alonso.IsInvincible())
            {
                newRenderer = invincibleFondoPrefab.GetComponent<SpriteRenderer>();
            }
            else
            {
                List<GameObject> stageFondos = new List<GameObject>();
                foreach (var fondo in fondoPrefabs)
                {
                    if (fondo != null && fondo.name.ToUpper().Contains(currentFondoName))
                    {
                        stageFondos.Add(fondo);
                    }
                }
                if (stageFondos.Count > 0)
                {
                    int idx = Random.Range(0, stageFondos.Count);
                    newRenderer = stageFondos[idx].GetComponent<SpriteRenderer>();
                }
            }
        }
        else if (alonso != null && alonso.IsInvincible())
        {
            newRenderer = invincibleFondoPrefab.GetComponent<SpriteRenderer>();
        }
        else
        {
            int randomIndex = Random.Range(0, fondoPrefabs.Length);
            newRenderer = fondoPrefabs[randomIndex].GetComponent<SpriteRenderer>();
        }

        if (currentRenderer != null && newRenderer != null)
        {
            currentRenderer.sprite = newRenderer.sprite;
        }
    }
}
