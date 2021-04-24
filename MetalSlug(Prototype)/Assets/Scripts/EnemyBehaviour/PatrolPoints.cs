using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolPoints : MonoBehaviour
{
    public List<Transform> points;
    public int next = 0;
    private int nextPoint = 1;
    EnemyBehaviour enemy;
    public Animator animate;
    private Rigidbody2D rb;
    public float speed;
    bool isMoving;

    private void Awake()
    {
        enemy = this.gameObject.GetComponent<EnemyBehaviour>();
        animate = enemy.GetComponent<Animator>();
        rb = enemy.GetComponent<Rigidbody2D>();
        speed = enemy.stats.speed;
    }
    private void Reset()
    {
        points = new List<Transform>();
    }

    private void Update()
    {
        MoveTo();
        if (enemy.isDead)
        {
            stopPatrol();
            enabled = false;
        }
    }
    void MoveTo()
    {
        isMoving = animate.GetBool("isMoving");
        //Get the next Point transform
        Transform goal = points[next];
        //Flip the enemy transform to look into the point's direction
        if (goal.transform.position.x > transform.position.x)
            transform.eulerAngles = new Vector3(0, 0, 0);
        else
            transform.eulerAngles = new Vector3(0, 180, 0);
        //Move the enemy towards the goal point
        if (isMoving == true)
        {
            transform.position = Vector2.MoveTowards(transform.position, goal.position, speed * Time.deltaTime);
        }
        else
        {
            //Debug.Log(isMoving);
            stopPatrol();
        }
       
        getNextPoint(goal);
        
    }
    void stopPatrol()
    {
        rb.velocity = Vector2.zero;
        animate.SetBool("isMoving", false);
        
    }
    void getNextPoint(Transform goal)
    {
        //Check the distance between enemy and goal point to trigger next point
        if (Vector2.Distance(transform.position, goal.position) < 0.2f)
        {
            //Check if we are at the end of the line (make the change -1)
            if (next == points.Count - 1)
                nextPoint = -1;
            //Check if we are at the start of the line (make the change +1)
            if (next == 0)
                nextPoint = 1;
            //Apply the change on the nextID
            next += nextPoint;
        }
    }
}
