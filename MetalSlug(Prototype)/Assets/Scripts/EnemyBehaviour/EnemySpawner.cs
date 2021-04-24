using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private Transform cam;
    private float distanceFromCam;
    public float speed;
    public List<GameObject> enemies;
    int randX;
    Vector2 whereToSpawn;
    public float spawnRate = 2.0f;
    float whenToStart = 2.5f;
    private GameObject playerRef;

    private void Spawn()
    {
        if (playerRef.activeSelf)
        {
            if (Time.time > whenToStart)
            {
                randX = (int)Random.Range(0, enemies.Count);
                whereToSpawn = this.transform.position;
                Instantiate(enemies[randX], whereToSpawn, Quaternion.Euler(0f, 180f, 0f));
            }
        }
        else
            GameManager.gm.StartCoroutine(GameManager.gm.WaitForSpawn(playerRef));
    }

    private void CamFollow()
    {
        if (Vector2.Distance(this.transform.position, cam.position) < distanceFromCam)
            this.transform.position = Vector2.MoveTowards(transform.position, new Vector2(cam.position.x, transform.position.y), -speed * Time.deltaTime);
    }

    private void Start()
    {
        cam = Camera.main.transform;
        distanceFromCam = Vector2.Distance(this.transform.position, cam.position);
        playerRef = GameObject.FindGameObjectWithTag("Player");
        InvokeRepeating("Spawn", whenToStart, spawnRate);
    }

    private void LateUpdate()
    {
        CamFollow();
    }
}
