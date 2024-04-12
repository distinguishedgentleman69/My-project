using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Variables for controlling player movement and physics
    private Rigidbody2D rb; // Reference to the player's Rigidbody component
    
    private Animator anim;

    [Header("movement info")]
    public float moveSpeed; // Speed at which the player moves horizontally
    public float jumpPower; // add a jump power in player script

    [SerializeField]private bool canDoubleJump;

    private bool canMove;
    [SerializeField]private float movingInput; // Input value for horizontal movement

     private bool facingRight = true;
    private int facingDirection = 1;

    public Vector2 wallJumpDirection;

    // Variables for checking ground collision

    [Header("Collision info")]
    public LayerMask whatIsGround; // Layer mask for identifying ground
    public float groundCheckDistance; // Distance to check for ground collision
    [SerializeField]private bool isGrounded; // check if the player is grounded

    public float wallCheckDistance;
    private bool isWallDetected;
    private bool canWallSlide;
    private bool isWallSliding;

    
   
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        AnimationControllers();
        CollisionCheck();
        FlipController();
        
        InputChecks();


        if(isGrounded)
        {
            canDoubleJump = true;
            canMove = true;
        }

        if(canWallSlide)
        {
            isWallSliding = true;
            rb.velocity =new Vector2(rb.velocity.x,rb.velocity.y * 0.1f);
        }


        // Move the player horizontally
        Move();
    }

    private void AnimationControllers()
    {
        bool isMoving = rb.velocity.x != 0;

        anim.SetBool("isMoving",isMoving);
        anim.SetFloat("yVelocity",rb.velocity.y);
        anim.SetBool("isGrounded",isGrounded);
        anim.SetBool("isWallDetected",isWallDetected);
        anim.SetBool("isWallSliding",isWallSliding);
    }

    private void InputChecks()
    {
        movingInput = Input.GetAxisRaw("Horizontal");

        if(Input.GetAxis("Vertical") < 0)
        {
            canWallSlide = false;
        }

        // Check if the player pressed the jump button
        if(Input.GetKeyDown(KeyCode.Space)){

            // If the player is grounded, allow them to jump
            JumpButton();
        }
    }

    private void JumpButton()
    {
        if(isWallSliding)
        {
            WallJump();
            canDoubleJump = true;
        }
        else if(isGrounded)
        {
            Jump();
        }
        else if(canDoubleJump)
        {
            canMove = true;
            canDoubleJump = false;
            Jump();
        }

        canWallSlide = false;
    }

    private void WallJump()
    {
        canMove = false;
        rb.velocity = new Vector2(wallJumpDirection.x * -facingDirection, wallJumpDirection.y);
    }

    // Function to handle horizontal movement
    private void Move()
    {
         // Set the player's velocity based on the input and current velocity
         if(canMove)
         rb.velocity = new Vector2(moveSpeed * movingInput ,rb.velocity.y);
    }

    // Function to handle player jumping
    private void Jump()
    {
        // Apply vertical force to make the player jump
        rb.velocity = new Vector2(rb.velocity.x,jumpPower);
    }

    private void FlipController()
    {
        if(facingRight && rb.velocity.x < -0.1f)
        {
            Flip();
        }
        else if (!facingRight && rb.velocity.x > 0.1f)
        {
            Flip();
        }
    }

    private void Flip()
    {
        facingDirection = facingDirection *-1;
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }
    // Function to check for ground collision
    private void CollisionCheck()
    {
        // Cast a ray downwards to check for ground collision
        // If the ray hits an object in the whatIsGround layer mask within the specified distance, set isGrounded to true
        isGrounded = Physics2D.Raycast(transform.position,Vector2.down,groundCheckDistance,whatIsGround);
        isWallDetected = Physics2D.Raycast(transform.position,Vector2.right * facingDirection , wallCheckDistance ,whatIsGround);

        if(isWallDetected && rb.velocity.y < 0)
        {
            canWallSlide = true;
        }

         if(!isWallDetected)
        {
            isWallSliding = false;
            canWallSlide = false;
            
        }


    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position,new Vector3(transform.position.x,transform.position.y - groundCheckDistance));
        Gizmos.DrawLine(transform.position,new Vector3(transform.position.x + wallCheckDistance * facingDirection, transform.position.y));
    }

    
}
