using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Shot : MonoBehaviour
{
    public float speed;
    private GameObject player;
    private Transform player_pos;
    private Vector2 target;
    private PlayerHealth PlayerHealth;
    private Animator clip;
    //public float bullet_life;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        player_pos = player.transform;
        target = new Vector2(player_pos.position.x, player_pos.position.y);
        clip = GetComponent<Animator>();
        //PlayerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
        Bullet_Destroy();       
    }

    private void Bullet_Destroy()
    {
        if(transform.position.x == target.x && transform.position.y == target.y)
        {
            clip.Play("bullet_explode");
            this.gameObject.SetActive(false);
        }
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if(player.activeSelf)
                PlayerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
            if (PlayerHealth.enabled == true)
            { 
                StartCoroutine(PlayerHealth.Hit());
            }
            this.gameObject.SetActive(false);
        }
        else if (collision.CompareTag("Ground"))
            this.gameObject.SetActive(false);
    }
}
