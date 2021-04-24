using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    private Rigidbody2D enemy;
    private Collider2D col;
    public float force;

    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponentInParent<Rigidbody2D>();
        col = this.gameObject.GetComponent<Collider2D>();
    }

   private void JumpOver()
    {
       // Debug.Log("Jump");
        enemy.AddForce(Vector2.up * force);
    }

    private IEnumerator Pause()
    {
        yield return new WaitForSeconds(3f);
        col.enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log(collision);
        if (this.gameObject.tag.Equals("WallCheck"))
        {
            if (collision.tag.Equals("Ground"))
            {
                col.enabled = false;
                JumpOver();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //Debug.Log(collision);
        if (this.gameObject.activeInHierarchy)
        {
            if (collision.tag != "Ground" && collision.isActiveAndEnabled)
                StartCoroutine(Pause());
        }
        else
            enabled = false;
    }
}
