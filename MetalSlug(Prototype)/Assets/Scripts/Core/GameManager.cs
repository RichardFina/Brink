using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//used to manage frequently used functions such as spawning and destroying objects
public class GameManager : MonoBehaviour
{
    //create partial singleton to prevent multiple instances
    public static GameManager gm;

    [SerializeField]
    private int numberOfLives;

    private static int remainingLives = 3;
    public static int RemainingLives
    {
        get { return remainingLives; }
    }

    private void Awake()
    {
        if (gm == null)
            gm = FindObjectOfType<GameManager>();
    }

    private void Start()
    {
        remainingLives = numberOfLives;
    }

    public GameObject playerPrefab;
    public PlayerHealth health;
    public Transform spawnPoint;
    public float spawnDelay = 1.5f;
    public GameObject currentCheckpoint;

    [SerializeField]
    private GameObject gameOverUI;
    public GameObject levelCompleteUI;

    public void EndGame()
    {
        Debug.Log("Game over");
        gameOverUI.SetActive(true);
    }
    public void CompleteLevel()
    {
        Debug.Log("Level Complete");
        levelCompleteUI.SetActive(true);
    }
    public IEnumerator RespawnPlayer(GameObject player)
    {
        //Debug.Log("Add spawn sound and animation");
        yield return new WaitForSeconds(spawnDelay);
        

        //Debug.Log(playerPrefab.name);
        player.transform.position = currentCheckpoint.transform.position;
        player.SetActive(true);
        health = player.GetComponent<PlayerHealth>();
        health.enabled = true;
        health.CurrentHealth = health.MaxHealth;
        Debug.Log("add effects");
    }

    //methods to kill player and enemy 
    public IEnumerator KillEnemy(EnemyBehaviour enemy)
    {
       // Debug.Log(enemy.animate.GetCurrentAnimatorStateInfo(0).length);
        yield return new WaitForSeconds(1);
        enemy.gameObject.SetActive(false);
    }
    
    public static void KillPlayer(GameObject player)
    {
        player.SetActive(false);
        remainingLives -= 1;
        Debug.Log(remainingLives);
        if (remainingLives <= 0)
        {
            gm.EndGame();
        }
        else
            gm.StartCoroutine(gm.RespawnPlayer(player));
    }

    public IEnumerator WaitForSpawn(GameObject player)
    {
        yield return new WaitForSeconds(spawnDelay);
        player = GameObject.FindGameObjectWithTag("Player");
    }
}
