using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
public class EnemyWalker : MonoBehaviour
{
    Rigidbody2D rb;
    SpriteRenderer sr;
    Animator anim;
    AudioSource enemyDeathAudioSource;

    public int health;
    public float speed;
    public AudioClip enemyDeathSFX;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        if (speed <= 0)
        {
            speed = 5.0f;
        }

        if (health <= 0)
        {
            health = 3;
        }
    }

    // Update is called once per frame

    void Update()
    {
        if (!anim.GetBool("Death") && !anim.GetBool("Squished"))
            if (sr.flipX)
            {
                rb.velocity = new Vector2(-speed, rb.velocity.y);
            }
            else
            {
                rb.velocity = new Vector2(speed, rb.velocity.y);

            }


    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Barrier")
        {
            sr.flipX = !sr.flipX;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Projectile")
        {
            health--;
            Destroy(collision.gameObject);
            if (health <= 0)
            {
                GameManager.instance.score ++;
                if (!enemyDeathAudioSource)
                {
                    enemyDeathAudioSource = gameObject.AddComponent<AudioSource>();
                    enemyDeathAudioSource.clip = enemyDeathSFX;
                    enemyDeathAudioSource.loop = false;
                    enemyDeathAudioSource.Play();
                }
                else
                {
                    enemyDeathAudioSource.Play();
                }
                transform.position = Vector3.one * 999999999f;
            }
        }
    }
}

   
