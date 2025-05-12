using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alonso : MonoBehaviour
{
    [SerializeField] private float upForce;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask ground;
    [SerializeField] private float radius;
    [SerializeField] private GameObject doubleJumpUI;
    [SerializeField] private GameObject invincibleUI;
    private Rigidbody2D aloRB;
    private Animator aloAnim;
    private bool canDoubleJump = false;
    private bool hasPowerUp = false;
    private bool isInvincible = false;
    private float invincibleDuration = 5f;
    private float invincibleTimer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        aloRB = GetComponent<Rigidbody2D>();
        aloAnim = GetComponent<Animator>();
        if (doubleJumpUI != null)
        {
            doubleJumpUI.SetActive(false);
        }
        if (invincibleUI != null)
        {
            invincibleUI.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        bool isGrounded = Physics2D.OverlapCircle(groundCheck.position, radius, ground);
        aloAnim.SetBool("isGrounded", isGrounded);

        if (isGrounded)
        {
            canDoubleJump = false;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded)
            {
                aloRB.AddForce(Vector2.up * upForce);
            }
            else if (hasPowerUp && !canDoubleJump)
            {
                aloRB.AddForce(Vector2.up * upForce * 1.5f);
                canDoubleJump = true; 
                hasPowerUp = false; 
                if (doubleJumpUI != null)
                {
                    doubleJumpUI.SetActive(false);
                }
            }
        }

        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer <= 0f)
            {
                isInvincible = false;
                if (invincibleUI != null)
                {
                    invincibleUI.SetActive(false);
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, radius);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Obstaculo"))
        {
            if (isInvincible)
            {
                Destroy(collision.gameObject);
            }
            else
            {
                GameManager.Instance.ShowGameOverScreen();
                aloAnim.SetTrigger("Die");
                Time.timeScale = 0f;
            }
        }
        else if (collision.gameObject.CompareTag("DobleSalto"))
        {
            hasPowerUp = true;
            if (doubleJumpUI != null)
            {
                doubleJumpUI.SetActive(true);
            }
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("Invencible"))
        {
            isInvincible = true;
            invincibleTimer = invincibleDuration;
            if (invincibleUI != null)
            {
                invincibleUI.SetActive(true);
            }
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("BlancaYNegra"))
        {
            GameManager.Instance.ApplyBlancaYNegraEffect(5f);
            Destroy(collision.gameObject);
        }
    }
}
