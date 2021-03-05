using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class EnemyTurret : MonoBehaviour
{
    SpriteRenderer enemyTurret;

    public Transform projectileSpawnPointLeft;
    public Transform projectileSpawnPointRight;
    public Projectile EnemyProjectilePrefab;
    public Transform PlayerDistance;
    public Transform Player;

    public float projectileForce;
    public float projectileFireRate;
    float timeSinceLastFire = 0;
    public int health;
    bool isFacingLeft = true;

    Animator anim;

    // Start is called before the first frame update
    void Start()
    {

        enemyTurret = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        if (projectileForce <= 0)
        {
            projectileForce = 7.0f;
        }

        if (health <= 0)
        {
            health = 1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector2.Distance(transform.position, PlayerDistance.transform.position);

        if (Time.time >= timeSinceLastFire + projectileFireRate && distance <= 10)
        {
            anim.SetBool("isFiring", true);
            timeSinceLastFire = Time.time;
        }

        if (transform.position.x < Player.position.x && isFacingLeft)
        {
            flip();

        }
        else if (transform.position.x > Player.position.x && !isFacingLeft)
        {
            flip();

        }


        if (Time.time > timeSinceLastFire + projectileFireRate)
        {
            Fire();
            timeSinceLastFire = Time.time;
        }

    }

    public void Fire()
    {

        if (isFacingLeft == true)
        {
            Projectile temp = Instantiate(EnemyProjectilePrefab, projectileSpawnPointLeft.position, projectileSpawnPointLeft.rotation);
            temp.speed = -projectileForce;
        }
        else
        {
            Projectile temp = Instantiate(EnemyProjectilePrefab, projectileSpawnPointLeft.position, projectileSpawnPointLeft.rotation);
            temp.speed = projectileForce;
        }
    }

    public void returnToIdle()
    {
        anim.SetBool("isFiring", false);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Projectile")
        {
            health--;
            Destroy(collision.gameObject);
            if (health <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    void flip()
    {
        if (isFacingLeft)
            isFacingLeft = false;
        else
            isFacingLeft = true;

        Vector3 scaleFactor = transform.localScale;

        scaleFactor.x *= -1;


        transform.localScale = scaleFactor;
    }
}
