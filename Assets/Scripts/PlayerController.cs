using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float speed = 8.0f;
    public float jumpForce = 8.0f;
    public int jumpsLimit = 2;

    private Rigidbody2D rb;
    private Animator anim;
    private bool isGrounded = false;
    private float initialXScale;
    private int jumpCount = 0;


    float horizontalVelocity;

    // Start is called before the first frame update
    void Start()
    {
        initialXScale = transform.localScale.x;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalVelocity = Input.GetAxisRaw("Horizontal");
        updateAnimations();
        jump();
    }

    private void FixedUpdate()
    {
        move();
    }

    private void move()
    {
        rb.velocity = new Vector2(horizontalVelocity * speed, rb.velocity.y);
        faceDir();
    }

    private void jump()
    {
        if (Input.GetButtonDown("Jump") && jumpCount > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            isGrounded = false;
            jumpCount--;
        }
    }

    private void faceDir()
    {
        if (horizontalVelocity != 0)
            transform.localScale = new Vector3(initialXScale * horizontalVelocity, transform.localScale.y, transform.localScale.z);
    }

    private void updateAnimations()
    {
        anim.SetFloat("Horizontal", horizontalVelocity);
        anim.SetFloat("Vertical", rb.velocity.y);
        anim.SetBool("isGrounded", isGrounded);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isGrounded = true;
            jumpCount = jumpsLimit;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}
