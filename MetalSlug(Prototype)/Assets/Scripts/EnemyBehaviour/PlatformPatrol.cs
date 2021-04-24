using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlatformPatrol : MonoBehaviour
{
    EnemyBehaviour enemy;
    private Animator animate;
    private Rigidbody2D rb;
    private float speed;
    private float distance = 1.0f;
    private bool movingRight = true;
    private RaycastHit2D hit;
    public Transform groundDetection;
    bool isMoving;

    public void Start()
    {
        enemy = this.gameObject.GetComponent<EnemyBehaviour>();
        speed = enemy.stats.speed;
        animate = enemy.GetComponent<Animator>();
        rb = enemy.GetComponent<Rigidbody2D>();
        //isMoving = enemy.isMoving;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        patrol();
        if (enemy.isDead)
        {
            stopPatrol();
            enabled = false;
        }
    }
    void patrol()
    {
        isMoving = animate.GetBool("isMoving");
        if (isMoving == true)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }
        else
        {
            stopPatrol();
        }
        hit = Physics2D.Raycast(groundDetection.position, Vector2.down, distance);
        if (hit.collider == null)
        {
            //Debug.Log(hit.collider);
            if (movingRight == true)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                movingRight = false;
                enemy.facingRight = false;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                movingRight = true;
                enemy.facingRight = true;
            }
        }
            //Debug.Log(hit.collider.name);
    }

    void stopPatrol()
    {
        rb.velocity = Vector2.zero;
        animate.SetBool("isMoving", false);
    
    }
}

