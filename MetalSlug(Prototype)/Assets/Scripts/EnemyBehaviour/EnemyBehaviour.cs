using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Animations;
using UnityEngine;

//class contains all the needed data and components of Enemy objects
public class EnemyBehaviour : MonoBehaviour
{
    [System.Serializable]
    public class EnemyStats
    {
        public int health;
        public float speed;
        public float atk_damage;
    }

    public EnemyStats stats = new EnemyStats();
    //animator component to control animations
    [HideInInspector]
    public Animator animate;
    //Rigidbody component to control movement
    [HideInInspector]
    public Rigidbody2D rb;
    [HideInInspector]
    public GameObject player;
    //audio source array to store enemy sounds
    public AudioSource[] sound;
    //bool for if enemy is attacking somethinh
    protected bool isAttacking = false;
    //bool for is the enemy is moving or not
    [HideInInspector]
    public bool isMoving = true;
    [HideInInspector]
    public bool isDead = false;
    [HideInInspector]
    public bool inRange = false;
    [HideInInspector]
    public bool facingRight;
    protected RaycastHit2D check_left;
    protected RaycastHit2D check_right;
    public Transform attack_check;
    public float distance = 1.0f;

    protected virtual void Start()
    {
        animate = this.gameObject.GetComponent<Animator>();
        sound = this.gameObject.GetComponents<AudioSource>();
        rb = this.gameObject.GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public virtual void Flip()
    {
        if (player.transform.position.x < this.transform.position.x && facingRight)
        {
            check_left = Physics2D.Raycast(attack_check.position, Vector2.left, distance);
            if (check_left.collider == null)
                inRange = false;
            else if (check_left.collider.tag.Equals("Player"))
            {
                transform.Rotate(new Vector3(0, 180, 0));
                facingRight = false;
            }
        }
        else if (player.transform.position.x > this.transform.position.x && !facingRight)
        {
            check_right = Physics2D.Raycast(attack_check.position, Vector2.right, distance);
            if (check_right.collider == null)
                inRange = false;
            else if (check_right.collider.tag.Equals("Player"))
            {
                transform.Rotate(new Vector3(0, 180, 0));
                facingRight = true;
            }
        }
    }
    // Debug.Log(inRange)

    //method to decrement the amount of health 
    //destroys enemy when health is zero
    public void takeDamage(int damage)
    {
        //decrement our max health by damage amount
        stats.health -= damage;
        if (!sound[0].isPlaying && stats.health > 0)
            sound[0].Play();
        if (stats.health <= 0)
        {
            //play death animation and sound
            isDead = true;
            isMoving = false;
            //Debug.Log(isDead);
            //  Debug.Log(isMoving);
            Dead();
            //call killenemy to destroy this
        }
    }

    public virtual void Dead()
    {
        animate.Play("death");
        animate.SetBool("isMoving", false);
        GameManager.gm.StartCoroutine(GameManager.gm.KillEnemy(this));
    }

    void FixedUpdate()
    {
        if (player.activeInHierarchy)
        {
            enabled = true;
            Flip();
        }
        else
        {
            GameManager.gm.StartCoroutine(GameManager.gm.WaitForSpawn(player));
        }

    }
}
