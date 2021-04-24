using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    public Rigidbody2D rb;
    private EnemyBehaviour enemy;

    // Update is called once per frame
    void Update()
    {
        rb.velocity = transform.right * speed;
        Destroy(gameObject, 0.5f);
    }

    void OnTriggerEnter2D(Collider2D hitInfo){
        //Debug.Log(hitInfo.gameObject.layer);
        if (hitInfo.gameObject.layer.Equals(14))
        {
            //Debug.Log(hitInfo);
            enemy = hitInfo.gameObject.GetComponent<EnemyBehaviour>();
            enemy.takeDamage(1);
            Destroy(gameObject);
        }
        /*if(enemy == null){
            return;
        }
        enemy.takeDamage(1);
        Destroy(gameObject);*/
    }
}
