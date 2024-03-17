using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Variables for controlling player movement and physics
    public float moveSpeed; // Speed at which the player moves horizontally
    public float jumpPower; // add a jump power in player script
    public Rigidbody2D rb; // Reference to the player's Rigidbody component

    [SerializeField]private bool canDoubleJump;
    [SerializeField]private float movingInput; // Input value for horizontal movement

    // Variables for checking ground collision
    public LayerMask whatIsGround; // Layer mask for identifying ground
    public float groundCheckDistance; // Distance to check for ground collision
    [SerializeField]private bool isGrounded; // check if the player is grounded
    
    
    void Start()
    {
        
    }

    void Update()
    {
        // Check for ground collision
        collisionCheck();
        
        // Read horizontal movement input from player
        movingInput = Input.GetAxisRaw("Horizontal");

        // Check if the player pressed the jump button
        if(Input.GetKeyDown(KeyCode.Space)){

            // If the player is grounded, allow them to jump
            JumpButton();
        }

        if(isGrounded)
        {
            canDoubleJump = true;
        }

        // Move the player horizontally
        Move();
    }

    private void JumpButton()
    {
        if(isGrounded)
        {
            Jump();
        }
        else if(canDoubleJump)
        {
            canDoubleJump = false;
            Jump();
        }
    }

    // Function to handle horizontal movement
    private void Move()
    {
         // Set the player's velocity based on the input and current velocity
         rb.velocity = new Vector2(moveSpeed * movingInput ,rb.velocity.y);
    }

    // Function to handle player jumping
    private void Jump()
    {
        // Apply vertical force to make the player jump
        rb.velocity = new Vector2(rb.velocity.x,jumpPower);
    }

    // Function to check for ground collision
    private void collisionCheck()
    {
        // Cast a ray downwards to check for ground collision
        // If the ray hits an object in the whatIsGround layer mask within the specified distance, set isGrounded to true
        isGrounded = Physics2D.Raycast(transform.position,Vector2.down,groundCheckDistance,whatIsGround);
    }
}
