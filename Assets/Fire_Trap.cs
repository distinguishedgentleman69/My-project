using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Fire : Trap
{
    public bool isWorking;
    private Animator anim;
    public float repeatRate;
    

    private void Start()
    {
        anim = GetComponent<Animator>();

        if(transform.parent == null)
        {
            InvokeRepeating("FireSwitch",0,repeatRate);
        }
    }

    private void Update()
    {
        anim.SetBool("isWorking",isWorking);
    }

    public void FireSwitch()
    {
        isWorking = !isWorking; 
    }

    public void FireSWitchAfter(float seconds)
    {
        CancelInvoke();
        isWorking = false;
        Invoke("FireSwitch",seconds);
    }
     protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if(isWorking)
            base.OnTriggerEnter2D(collision);
    }
}
