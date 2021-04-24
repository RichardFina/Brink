using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgunner : EnemyBehaviour
{
    [SerializeField]
    private int bulletAmount = 10;

    [SerializeField]
    private float startAngle = 90f, endAngle = 270f;

    [SerializeField]
    private float upAngle = 90f, endUpAngle = 270f;

    //private EnemyBehaviour enemy;
    public Transform explosion;
    public Transform FirePoint;
    public float stoppingDistance;
    public float retreatDistance;
    public float fireDistance;
    private float reload_time;
    public float time_before_fire;
    private Transform player_pos;

    private float fire_origin;
    private float fire_up;

    protected override void Start()
    {
        base.Start();
        player_pos = player.transform;
        reload_time = time_before_fire;
        fire_origin = FirePoint.position.y;
        fire_up = -1.19f;
        //enemy = GetComponent<EnemyBehaviour>();
    }

    private void Reload()
    {
        reload_time -= Time.deltaTime;
    }

    private void GetFireAngle(ref float angle, ref float angleStep)
    {
        if (player_pos.position.y <= transform.position.y)
        {
            angleStep = (endAngle - startAngle) / bulletAmount;
            angle = startAngle;
        }
        else if (player_pos.position.y > transform.position.y)
        {
            angleStep = (endUpAngle - upAngle) / bulletAmount;
            angle = upAngle;
        }
    }

    private void Fire()
    {
        float angleStep = 0f;
        float angle = 0f;
        GetFireAngle(ref angle, ref angleStep);
        //Debug.Log(angle);

        for(int i = 0; i < bulletAmount + 1; i++)
        {
            float bulDirX = transform.position.x + Mathf.Sin((angle * Mathf.PI)/180f);
            float bulDirY = transform.position.y + Mathf.Cos((angle * Mathf.PI) / 180f);

            Vector3 moveVector = new Vector3(bulDirX, bulDirY, 0f);
            Vector2 bulDir = (moveVector - transform.position).normalized;

            GameObject bul = bulletpool.poolInstance.GetBullet();
            bul.transform.position = FirePoint.transform.position;
            bul.transform.rotation = FirePoint.transform.rotation;
            bul.SetActive(true);
            bul.GetComponent<pellet>().SetMoveDirection(bulDir);

            angle += angleStep;
        }
    }

    bool DoNotFire()
    {
        if (isMoving && (int)player_pos.position.y > (int)transform.position.y)
        {
            return false;
        }
        else if (isMoving && (int)player_pos.position.y < (int)transform.position.y)
        {
            return false;
        }
        else if (isMoving && ((int)player_pos.position.y == (int)transform.position.y))
            return true;
        else
            return true;
    }

    void Chase_Flee_Fire()
    {
        Vector2 stopVector = new Vector2(transform.position.x, 0f);
        Vector2 playerX = new Vector2(player_pos.transform.position.x, 0f);
        //Debug.Log(Vector2.Distance(stopVector, playerX));
        if(Vector2.Distance(stopVector, playerX) > stoppingDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(player_pos.position.x, transform.position.y), stats.speed * Time.deltaTime);
            isMoving = true;
            //Debug.Log("this fires");
        }
        else if(Vector2.Distance(stopVector, playerX) < stoppingDistance &&
            Vector2.Distance(stopVector, playerX) > retreatDistance)
        {
            transform.position = this.transform.position;
            rb.velocity = Vector2.zero;
            isMoving = false;
           // Debug.Log(isMoving);
        }
        else if(Vector2.Distance(transform.position, player_pos.position) < retreatDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(player_pos.position.x, transform.position.y), -stats.speed * Time.deltaTime);
            isMoving = true;
        }

        if (Vector2.Distance(transform.position, player_pos.position) <= fireDistance)
        {
            bool fire = DoNotFire();
            //Debug.Log(fire);
            if(fire)
            { 
                if (reload_time <= 0)
                {
                    Fire();
                    reload_time = time_before_fire;
                    if (!sound[1].isPlaying)
                        sound[1].Play();
                }
                else
                    Reload();
            }
        }

    }

    public override void Flip()
    {
        base.Flip();
        int layerMask = 1 << 9;
        RaycastHit2D check_up = Physics2D.Raycast(attack_check.position, Vector2.up, distance, layerMask);
        if (check_up.collider == null)
        {
            //Debug.Log("not hitting anything important");
        }
        else if(check_up.collider.tag.Equals("Player"))
        {
            if(facingRight)
            {
                transform.Rotate(new Vector3(0, 180, 0));
                facingRight = false;
            }
            else
            {
                transform.Rotate(new Vector3(0, 180, 0));
                facingRight = true;
            }
        }
    }

    private void AnimationControl()
    {
        //Debug.Log(enemy.isMoving);
        if (isMoving)
        {
            animate.Play("Run");
            FirePoint.position = new Vector2(FirePoint.position.x, FirePoint.parent.localPosition.y);
        }
        else if (!isMoving && (transform.position.y < player_pos.transform.position.y))
        {
            animate.Play("UpForward");
            FirePoint.position = new Vector3(FirePoint.position.x, fire_up, 0f);
        }
        /*else if (isMoving && (transform.position.y < player_pos.transform.position.y))
        {
            animate.Play("up-aim");
            FirePoint.position = new Vector3(FirePoint.position.x, fire_up, 0f);
        }*/
        else if (!isMoving)
        {
            animate.Play("Crouch-Fire");
            FirePoint.position = new Vector2(FirePoint.position.x, FirePoint.parent.localPosition.y);
        }
    }

    private void Update()
    {
        if (player.activeSelf)
            Chase_Flee_Fire();
        else
            GameManager.gm.StartCoroutine(GameManager.gm.WaitForSpawn(player));

    }
    private void LateUpdate()
    {
        if (player_pos.gameObject.activeSelf)
            AnimationControl();
        else
            GameManager.gm.StartCoroutine(GameManager.gm.WaitForSpawn(player));
    }

    public override void Dead()
    {
        if(explosion)
        {
            GameObject explode = ((Transform)Instantiate(explosion, this.transform.position, this.transform.rotation)).gameObject;
            //Destroy(explode, 2.0f);
        }
        GameManager.gm.StartCoroutine(GameManager.gm.KillEnemy(this));
    }
}
