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
    [SerializeField] private AudioSource jumpSound;
    [SerializeField] private AudioSource invincibleSong;
    [SerializeField] private AudioSource deathSound;
    private AudioSource mainCameraAudio;
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

        mainCameraAudio = Camera.main.GetComponent<AudioSource>();
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
                if (jumpSound != null)
                {
                    jumpSound.Play();
                }
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
                if (jumpSound != null)
                {
                    jumpSound.Play();
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

                if (mainCameraAudio != null)
                {
                    mainCameraAudio.mute = false;
                }
                if (invincibleSong != null && invincibleSong.isPlaying)
                {
                    invincibleSong.Stop();
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
                if (deathSound != null)
                {
                    deathSound.Play();
                }

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

            if (mainCameraAudio != null)
            {
                mainCameraAudio.mute = true;
            }
            if (invincibleSong != null)
            {
                invincibleSong.Play();
            }

            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("Verstappen"))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Final");
        }
    }

    public bool IsInvincible()
    {
        return isInvincible;
    }
}
