using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Animator animator;
    public float speed = 10f;
    public float jumpHeight= 5f;
    private Rigidbody2D rb2d;
    private BoxCollider2D bc2d;
    private bool isLeft = false;
    private bool isJumping = false;
    private bool isWalking = false;
    int playerLayer, platformLayer, wallLayer;
    bool jumpOffCoroutineIsRunning = false;
    bool isGrounded = true;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        bc2d = GetComponent<BoxCollider2D>();
        playerLayer = LayerMask.NameToLayer("Player");
        platformLayer = LayerMask.NameToLayer("Platform");
        wallLayer = LayerMask.NameToLayer("Wall");
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("Horizontal", Input.GetAxis("Horizontal"));
        animator.SetBool("isJumping", isJumping);
        animator.SetBool("isWalking", isWalking);
        float x = Input.GetAxis("Horizontal");
        rb2d.velocity = new Vector2 (x*speed, rb2d.velocity.y);
        if(Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow))
        {
            isWalking = true;
        }
        else isWalking = false;
        if(Input.GetButtonDown("Jump") && !Input.GetKey (KeyCode.DownArrow) && isGrounded == true) {
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, jumpHeight), ForceMode2D.Impulse);
        }
        else if(Input.GetButtonDown("Jump") && Input.GetKey(KeyCode.DownArrow) && isGrounded == true)
        {
            StartCoroutine ("JumpOff");
        }
		
		if (Input.GetKeyDown("left") && !isLeft){
            isLeft = true;
			transform.Rotate(0f, 180f, 0f);
		} else if (Input.GetKeyDown("right") && isLeft){
             isLeft = false;
			transform.Rotate(0f, 180f, 0f);
		}
		
        if (rb2d.velocity.y > 1 && !isGrounded)
			Physics2D.IgnoreLayerCollision(playerLayer, platformLayer, true);
		
		else if (rb2d.velocity.y <= 0 && !jumpOffCoroutineIsRunning)
			Physics2D.IgnoreLayerCollision(playerLayer, platformLayer, false);
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.collider.gameObject.layer == LayerMask.NameToLayer("Ground") || col.collider.gameObject.layer == LayerMask.NameToLayer("Platform"))
        {
            isGrounded = true;
            isJumping = false;
        }
        if (col.collider.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            Debug.Log("Toched Wall");
        }
    }
    void OnCollisionExit2D(Collision2D col)
    {
        if(col.collider.gameObject.layer == LayerMask.NameToLayer("Ground") || col.collider.gameObject.layer == LayerMask.NameToLayer("Platform"))
        {
            isGrounded = false;
            isJumping = true;
        }
    }
    IEnumerator JumpOff()
	{
		jumpOffCoroutineIsRunning = true;
		Physics2D.IgnoreLayerCollision(playerLayer, platformLayer, true);
		yield return new WaitForSeconds (0.5f);
		Physics2D.IgnoreLayerCollision(playerLayer, platformLayer, false);
		jumpOffCoroutineIsRunning = false;
	
        float Dirx = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        transform.position = new Vector2(transform.position.x + Dirx, transform.position.y);
        if (Input.GetButtonDown("Jump") && !Input.GetKey(KeyCode.DownArrow) && rb2d.velocity.y == 0)
        {
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, jumpHeight), ForceMode2D.Impulse);
        }
        else if (Input.GetButtonDown("Jump") && Input.GetKey(KeyCode.DownArrow) && rb2d.velocity.y == 0)
        {
            StartCoroutine("JumpOff");
        } 
		
        if (rb2d.velocity.y > 0)
            Physics2D.IgnoreLayerCollision(playerLayer, platformLayer, true);

        else if (rb2d.velocity.y <= 0 && !jumpOffCoroutineIsRunning)
            Physics2D.IgnoreLayerCollision(playerLayer, platformLayer, false);
    }
    IEnumerator Stop()
    {
        rb2d.velocity = new Vector2(0,0);
        yield return new WaitForSeconds(0);
    }
}