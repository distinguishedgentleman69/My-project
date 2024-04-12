using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Saw_Trap : Trap
{
    private Animator anim;
    private bool isWorking;
    [SerializeField] private Transform[] movePoint;
    [SerializeField] private float speed;
    private int movePointIndex;
    private float cooldownTimer;
    [SerializeField] private float cooldown;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        cooldownTimer-=Time.deltaTime;
        bool isWorking = cooldownTimer < 0;
        anim.SetBool("isWorking",isWorking);

        if(isWorking)
        {
            transform.position = Vector3.MoveTowards(transform.position,movePoint[movePointIndex].position,speed * Time.deltaTime);
        }
        if(Vector2.Distance(transform.position,movePoint[movePointIndex].position) < 0.15f)
        {
            cooldownTimer = cooldown;
            movePointIndex++;

            if(movePointIndex >= movePoint.Length)
            {
                movePointIndex = 0;
            }
        }
    }
}
