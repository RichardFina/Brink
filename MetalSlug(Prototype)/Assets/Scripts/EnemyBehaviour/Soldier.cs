using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : EnemyBehaviour
{
    protected float btwnShots;
    public float reload;
    public Transform Firepoint;
    public GameObject bullet;
    private GameObject player_check;
    private Transform player_pos;
    private AudioSource fireSound;
    public float fireDistance;
    public float retreatDistance;

    private void Awake()
    {
        fireSound = sound[1];
    }
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        player_check = player;
        player_pos = player_check.transform;
        btwnShots = reload;
    }

    // Update is called once per frame
    void Update()
    {
        if(isDead)
        {
            enabled = false;
        }
        if (player_check.activeInHierarchy)
        {
            FireRange();
            Retreat();
        }
        else
            GameManager.gm.StartCoroutine(GameManager.gm.WaitForSpawn(player));
    }

    public override void Flip()
    {
        if(inRange)
        {
            if (facingRight && (player_pos.transform.position.x < this.transform.position.x))
            {
                transform.Rotate(new Vector3(0, 180, 0));
                facingRight = false;
            }
            else if (!facingRight && (player_pos.transform.position.x > this.transform.position.x))
            {
                transform.Rotate(new Vector3(0, 180, 0));
                facingRight = true;
            }
        }
    }
    Quaternion rotateBullet()
    {
        Vector3 dir;
        float angle;
        Quaternion rotation;
        if (player_pos.position.y < Firepoint.position.y)
        {
            dir = Firepoint.position - player_pos.position;
            angle = Vector3.Angle(Firepoint.position, dir);
            rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        else
        {
            dir = player_pos.position - Firepoint.position;
            angle = Vector3.Angle(dir, Firepoint.position) - 180.0f;
            rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        return rotation;
    }

    protected virtual void Fire()
    {
        if(btwnShots <= 0)
        {
            Quaternion rotation = rotateBullet();
            Instantiate(bullet, Firepoint.transform.position, rotation);
            //Debug.Log(rotation);
            btwnShots = reload;
            //if (!sound[1].isPlaying)
            fireSound.Play();
        }
        else
        {
            btwnShots -= Time.deltaTime;
        }
    }

    protected void FireRange()
    {
        if (Vector2.Distance(transform.position, player_pos.position) <= fireDistance)
        {
            isMoving = false;
            inRange = true;
            Flip();
            Fire();
            this.transform.position = transform.position;
            rb.velocity = Vector2.zero;
        }
        else
        {
            //isMoving = true;
            inRange = false;
        }
    }

    protected void Retreat()
    {
        if (this.tag.Equals("PlatformSoldier"))
            retreatDistance = 0f;
        if (Vector2.Distance(transform.position, player_pos.position) < retreatDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(player_pos.position.x, transform.position.y), -stats.speed * Time.deltaTime);
            isMoving = true;
        }
    }

    private void AnimationController()
    {
        if (isMoving)
            animate.Play("run");
        else if (!isMoving)
            animate.Play("Aim");
    }

    private void LateUpdate()
    {
        AnimationController();
    }
}
