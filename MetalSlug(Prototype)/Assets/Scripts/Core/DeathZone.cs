using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.name);
        Debug.Log(gameObject.activeSelf);
        if (collision.gameObject.tag == "Player")
        {
            GameManager.KillPlayer(collision.gameObject);
            //Debug.Log(gameObject.activeSelf);
            /*if(GameManager.RemainingLives <= 0)
            {
                GameManager.gm.EndGame();
            }
            else
                GameManager.gm.StartCoroutine(GameManager.gm.RespawnPlayer(collision.gameObject));*/
        }
    }
}
