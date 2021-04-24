using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    Soldier soldier;
    private float distance = 1.0f;
    private bool movingRight;
    private RaycastHit2D hit;
    public Transform groundDetection;
    // Start is called before the first frame update
    void Start()
    {
        soldier = this.GetComponent<Soldier>();
        movingRight = soldier.facingRight;
        soldier.isMoving = true;
    }

    void FixedUpdate()
    {
        Patrol_check();
        if (soldier.isDead)
        {
            stopPatrol();
            enabled = false;
        }
    }

    void stopPatrol()
    {
        soldier.rb.velocity = Vector2.zero;
        soldier.isMoving = false;

    }

    /*IEnumerator Wait()
    {
        Debug.Log("wait");
        float distance = 5.0f;
        RaycastHit2D hit;
        hit = Physics2D.Raycast(transform.position, Vector2.left, distance);
        Debug.DrawRay(transform.position, Vector2.left, Color.red, distance);
        yield return new WaitForSecondsRealtime(3f);
        hit = Physics2D.Raycast(transform.position, Vector2.left, distance);
        Debug.DrawRay(transform.position, Vector2.left, Color.blue, distance);
    }*/

    void Patrol_check()
    {
        if (soldier.isMoving == true)
        {
            transform.Translate(Vector2.right * soldier.stats.speed * Time.deltaTime);
        }
        else
            soldier.isMoving = true;

        hit = Physics2D.Raycast(groundDetection.position, Vector2.down, distance);
        if (hit.collider == null)
        {
            //StartCoroutine("Wait");
            movingRight = soldier.facingRight;
            //Debug.Log(movingRight);
            if (!soldier.inRange)
            {
                if (movingRight == true)
                {
                    transform.eulerAngles = new Vector3(0, -180, 0);
                    //movingRight = false;
                    soldier.facingRight = false;
                }
                else
                {
                    transform.eulerAngles = new Vector3(0, 0, 0);
                    // movingRight = true;
                    soldier.facingRight = true;
                }
            }
        }
    }
}
