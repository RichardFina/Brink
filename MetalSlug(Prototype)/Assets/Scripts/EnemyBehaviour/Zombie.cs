using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Zombie : EnemyBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
           // Debug.Log(collision.name);
            isAttacking = true;
            isMoving = false;
            attackPlayer();
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        isAttacking = false;
        isMoving = true;
        attackPlayer();
    }

    private void attackPlayer()
    {
        if (isAttacking)
        {
            animate.SetBool("isAttacking", true);
            animate.SetBool("isMoving", false);
            if(!sound[1].isPlaying)
                sound[1].Play();
        }
        else
        {
            animate.SetBool("isAttacking", false);
            animate.SetBool("isMoving", true);
        }
    }

   
}
