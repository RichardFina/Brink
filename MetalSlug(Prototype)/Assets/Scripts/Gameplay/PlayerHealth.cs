using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public PlayerController playerController;
    public float MaxHealth = 10f;
    public float CurrentHealth = 10f;
    public float iframes = 3f;
    private BoxCollider2D bc2d;
    private GameObject player;
    //public int lives = 3;
    bool damageable = true;

    // Start is called before the first frame update
    void Start()
    {
        bc2d = GetComponent<BoxCollider2D> ();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        //if(col.gameObject.layer == LayerMask.NameToLayer("Enemy") && damageable)
        if(col.gameObject.name.Equals("Hand"))
        {
            if(player.activeInHierarchy)
                StartCoroutine(Hit());
        }
    }
    public IEnumerator Hit()
    {
        CurrentHealth = CurrentHealth -1;
        damageable = false;
        if(CurrentHealth <= 0)
        {
            damageable = true;
            GameManager.KillPlayer(this.gameObject);
            //GameManager.gm.StartCoroutine(GameManager.gm.RespawnPlayer(this.gameObject));
            //StartCoroutine(Dead());
        }
        else
        {
            yield return new WaitForSeconds(iframes);
            damageable = true;
        }
    }
   /* IEnumerator Dead()
    {
        Debug.Log("DEAD");
        GetComponent<Renderer>().enabled = false;
        yield return new WaitForSeconds(3);
        Debug.Log("RESPAWN");
        GetComponent<Renderer>().enabled = true;
        CurrentHealth = 10f;
        damageable = true;
    }*/
}

