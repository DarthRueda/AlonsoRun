using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FondoRepeat : MonoBehaviour
{
    [SerializeField] private GameObject[] fondoPrefabs;
    [SerializeField] private GameObject invincibleFondoPrefab;
    private float spriteWidth;
    private Alonso alonso;

    void Start()
    {
        alonso = FindObjectOfType<Alonso>();
        BoxCollider2D groundCollider = GetComponent<BoxCollider2D>();
        spriteWidth = groundCollider.size.x;
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
        SpriteRenderer newRenderer;

        if (alonso != null && alonso.IsInvincible())
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
