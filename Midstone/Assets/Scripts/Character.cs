using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {

    Rigidbody2D rb;
    public float speed;
    public float jumpForce;
    public bool isGrounded;
    public LayerMask isGroundLayer;
    public Transform groundCheck;
    public float groundCheckRadius;
    private float inputX;

    Animator anim;

    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        if (!rb)
        {
            Debug.Log("No Rigidbody2D found.");
        }
        else
        {
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            rb.mass = 2.0f;
        }

        if (speed <= 0)
        {
            speed = 5.0f;
            Debug.Log("Speed not set. Defaulting to " + speed);
        }

        if (jumpForce <= 0)
        {
            jumpForce = 15.0f;
            Debug.Log("JumpForce not set. Defaulting to " + jumpForce);
        }

        if (!groundCheck)
        {
            Debug.Log("No GroundCheck found.");
        }

        if (groundCheckRadius <= 0)
        {
            groundCheckRadius = 0.1f;
            Debug.Log("GroundCheckRadius not set. Defaulting to " + groundCheckRadius);
        }

        anim = GetComponent<Animator>();
        if (!anim)
        {
            Debug.Log("No Animator found on " + name);
        }
    }
	
	// Update is called once per frame
	void Update () {
        isGrounded = IsGrounded();
        anim.SetBool("Grounded", isGrounded);

        float moveValue = Input.GetAxisRaw("Horizontal");
        isGrounded = Physics2D.OverlapCircle(groundCheck.position,
            groundCheckRadius, isGroundLayer);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            anim.SetTrigger("Jump");
            Debug.Log("I was pressed");
        }

        else if (Input.GetMouseButtonDown(0))
        {
            anim.SetTrigger("Attack");
            Debug.Log("I was pressed");
        }

        else if (Mathf.Abs(inputX) > Mathf.Epsilon && isGrounded)
            anim.SetInteger("AnimState", 2);

        rb.velocity = new Vector2(moveValue * speed, rb.velocity.y);

        //anim.SetFloat("MoveSpeed", Mathf.Abs(moveValue));

        /*if (moveValue < 0 && !isFacingLeft)
            flip();
        else if (moveValue > 0 && isFacingLeft)
            flip();
            */
    }

    bool IsGrounded()
    {
        return Physics2D.Raycast(transform.position, -Vector3.up, 0.03f);
    }
}
