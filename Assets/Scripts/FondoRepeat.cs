using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FondoRepeat : MonoBehaviour
{
    [SerializeField] private GameObject[] fondoPrefabs;
    private float spriteWidth;

    void Start()
    {
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

        int randomIndex = Random.Range(0, fondoPrefabs.Length);

        SpriteRenderer currentRenderer = GetComponent<SpriteRenderer>();
        SpriteRenderer newRenderer = fondoPrefabs[randomIndex].GetComponent<SpriteRenderer>();

        if (currentRenderer != null && newRenderer != null)
        {
            currentRenderer.sprite = newRenderer.sprite;
        }
    }
}
