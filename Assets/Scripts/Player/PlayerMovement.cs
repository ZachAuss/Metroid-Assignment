using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;
    Animator anim;
    SpriteRenderer playerSprite;

    public float speed;
    public int jumpForce;
    public bool isGrounded;
    public bool isFiring;
    public LayerMask isGroundLayer;
    public Transform groundCheck;
    public float groundCheckRadius;
    

    // Start is called before the first frame update
    void Start()
    {
        
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        playerSprite = GetComponent<SpriteRenderer>();

        if (speed <= 0)
        {
            speed = 5.0f;
        }

        if (jumpForce <= 0)
        {
            jumpForce = 100;
        }

        if (groundCheckRadius <= 0)
        {
            groundCheckRadius = 0.01f;
        }

        if (!groundCheck)
        {
            Debug.Log("Groundcheck does not exist, please set a transform value for groundcheck");
        }
        

}

    // Update is called once per frame
    void Update()
    {
       
    float horizontalInput = Input.GetAxisRaw("Horizontal");
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, isGroundLayer);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.velocity = Vector2.zero;
            rb.AddForce(Vector2.up * jumpForce);
        }


        if (Input.GetButtonDown("Fire1"))
        {
            isFiring = true;
        }
        else if (Input.GetButtonUp("Fire1"))
        {
            isFiring = false;
        }

        rb.velocity = new Vector2(horizontalInput * speed, rb.velocity.y);
        anim.SetFloat("speed", Mathf.Abs(horizontalInput));
        anim.SetBool("isGrounded", isGrounded);
        anim.SetBool("isFiring", isFiring);

        if (playerSprite.flipX && horizontalInput > 0 || !playerSprite.flipX && horizontalInput < 0)
            playerSprite.flipX = !playerSprite.flipX;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            GameManager.instance.lives--;
        }

        if (collision.gameObject.tag == "EnemyProjectile")
        {
            GameManager.instance.lives--;
            Destroy(collision.gameObject);
        }
    }
   

    public void StartJumpForceChange()
    {
        StartCoroutine(JumpForceChange());
    }

    IEnumerator JumpForceChange()
    {
        jumpForce = 500;
        yield return new WaitForSeconds(2.0f);
        jumpForce = 200;

    }
    public void SpeedChange()
    {
        StartCoroutine(SpeedForceChange());
    }

    IEnumerator SpeedForceChange()
    {
        speed = 10;
        yield return new WaitForSeconds(3.0f);
        speed = 5;
    }
}
