using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{

    public float speed = 8.0f;

    private Rigidbody2D rb;
    private Animator anim;

    float horizontalVelocity;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalVelocity = Input.GetAxisRaw("Horizontal");
        updateAnimations();
    }

    private void FixedUpdate()
    {
        move();
    }

    private void move()
    {
        rb.velocity = new Vector2(horizontalVelocity * speed, rb.velocity.y);
    }

    private void updateAnimations()
    {
        anim.SetFloat("Horizontal", horizontalVelocity);
    }
}
