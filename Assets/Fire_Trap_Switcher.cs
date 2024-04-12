using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire_Trap_Switcher : MonoBehaviour
{
   public Fire myTrap;
   private Animator anim;

   private void Start()
   {
        anim = GetComponent<Animator>();
   }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<Player>() !=null)
        {
            anim.SetTrigger("pressed");
           myTrap.FireSWitchAfter(5); 
        }
    }
}
