using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed;
    public float jump;
    public Rigidbody2D rb;
    private float movingInput;

    public LayerMask whatIsGround;
    public float groundCheckDistance;
    private bool isGrounded;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics2D.Raycast(transform.position,Vector2.down,groundCheckDistance,whatIsGround);
        
        movingInput = Input.GetAxisRaw("Horizontal");

        if(Input.GetKeyDown(KeyCode.Space)){
            rb.velocity = new Vector2(rb.velocity.x,jump);
        }
        rb.velocity = new Vector2(moveSpeed * movingInput ,rb.velocity.y);
    }

    
}
