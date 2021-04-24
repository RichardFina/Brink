using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pellet : MonoBehaviour
{
    private Vector2 moveDirection;
    private float moveSpeed;
    private PlayerHealth PlayerHealth;

    private void OnEnable()
    {
        Invoke("Destroy", 1.5f);
    }

    private void Start()
    {
        Physics2D.IgnoreLayerCollision(0, 14);
        moveSpeed = 5f;
        PlayerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
    }

    private void Update()
    {
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
    }

    public void SetMoveDirection(Vector2 dir)
    {
        moveDirection = dir;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log(collision.tag);
        if (!this.gameObject.activeSelf)
            this.gameObject.SetActive(true);
        if (collision.CompareTag("Player") && collision.gameObject.activeInHierarchy)
        {
            StartCoroutine(PlayerHealth.Hit());
            this.gameObject.SetActive(false);
        }
        else if (collision.CompareTag("Ground"))
            this.gameObject.SetActive(false);
    }

    private void Destroy()
    {
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        CancelInvoke();
    }
}
