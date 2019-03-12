using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float jumpForce;
    private float moveInput;

    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;
    private bool isGrounded;

    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;
    bool jumpRequest;
    private Rigidbody2D rb;
    private bool facingRight = true;

    void Start()
    {
        speed *= 100;
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        moveInput = Input.GetAxisRaw("Horizontal");
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            jumpRequest = true;
        }
    }
    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);
        rb.velocity = new Vector2(moveInput * speed * Time.deltaTime, rb.velocity.y);
        if (!facingRight && moveInput > 0)
        {
            Flip();
        }
        else if (facingRight && moveInput < 0)
        {
            Flip();
        }
        Jump();
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }

    void Jump()
    {
        if (jumpRequest)
        {
            rb.AddForce(Vector2.up * jumpForce * Time.deltaTime, ForceMode2D.Impulse);
            jumpRequest = false;
        }
        if (rb.velocity.y < 0)
        {
            rb.gravityScale = fallMultiplier;
        }
        else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            rb.gravityScale = lowJumpMultiplier;
        }
        else
        {
            rb.gravityScale = 1f;
        }
    }
}
