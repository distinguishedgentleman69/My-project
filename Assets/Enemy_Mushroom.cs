using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Mushroom : Enemy
{
    [SerializeField] private float speed;
    [SerializeField] private float idleTime = 2;
    [SerializeField] private float idleTimeCounter;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        facingDirection = -1;
    }

    // Update is called once per frame
    void Update()
    {
        if(idleTimeCounter <= 0)
        {
            rb.velocity = new Vector2(speed *facingDirection, rb.velocity.y);
        }

        idleTimeCounter -= Time.deltaTime;
        CollisionChecks();

        if(wallDetected || !groundDetected)
        {
            Flip();
            idleTimeCounter = idleTime;
        }

        anim.SetFloat("xVelocity",rb.velocity.x);
    }
}
