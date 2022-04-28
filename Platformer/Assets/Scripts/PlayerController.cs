using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    //Floats
    public float maxSpeed;
    public float speed;
    [Range(1, 100)]
    public float jumpForce;
    public float invulnTimer;
    private float tempTimer = 0;

    //Booleans
    public bool grounded;
    public bool checkDoubleJump;
    public bool Invulnerable;

    //Reference
    private Rigidbody2D RB2D;
    private Animator playerAnimation;
    private GameMaster gm;

    //Stats
    public int currentHealth;
    [Range(1, 10)]
    public int maxHealth;

    void Start()
    {
        RB2D = gameObject.GetComponent<Rigidbody2D>();
        playerAnimation = gameObject.GetComponent<Animator>();

        currentHealth = maxHealth;
        gm = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<GameMaster>();
    }

    void Update()
    {
        RB2D.freezeRotation = true;
        playerAnimation.SetBool("Grounded", grounded);
        playerAnimation.SetFloat("Speed", Mathf.Abs(RB2D.velocity.x));

        if (Input.GetAxis("Horizontal") < -0.1f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        if (Input.GetAxis("Horizontal") > 0.1f)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }

        if (Input.GetButtonDown("Jump"))
        {
            if (grounded)
            {
                RB2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                checkDoubleJump = true;
            }
            else if (checkDoubleJump)
            {
                checkDoubleJump = false;
                RB2D.velocity = new Vector2(RB2D.velocity.x, 0);
                RB2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            }
        }

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        if (currentHealth <= 0)
        {
            GameOver();
            Destroy(this.gameObject);
        }

        if (Invulnerable && tempTimer < invulnTimer)
        {
            tempTimer += Time.deltaTime;
        }
        else
        {
            Invulnerable = false;
            tempTimer = 0;
        }
    }

    private void FixedUpdate()
    {
        Vector3 friction = RB2D.velocity;
        friction.y = RB2D.velocity.y;
        friction.z = 0.0f;
        friction.x *= 0.75f;

        float moveHorizontal = Input.GetAxis("Horizontal");

        //Creating friction to ease player movement (Note: Fake friction)
        if (grounded)
        {
            RB2D.velocity = friction;
        }

        RB2D.AddForce(Vector2.right * speed * moveHorizontal);

        //Limit speed
        if (RB2D.velocity.x > maxSpeed)
        {
            RB2D.velocity = new Vector2(maxSpeed, RB2D.velocity.y);
        }

        if (RB2D.velocity.x < -maxSpeed)
        {
            RB2D.velocity = new Vector2(-maxSpeed, RB2D.velocity.y);
        }
    }

    public void GameOver()
    {
        gm.gameOver = true;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Coin"))
        {
            Destroy(col.gameObject);
            gm.points += 5;
        }

        if (col.CompareTag("Victory"))
        {
            gm.victory = true;
        }
    }

    public void TakeDamage(int dmg)
    {
        if (!Invulnerable)
        {
            currentHealth -= dmg;

            Invulnerable = true;

            Destroy(transform.GetComponent<MushroomPowerUp>());
        }
    }

    public IEnumerator Knockback(float knockbackDur, float knockBackPowerY, float knockBackPowerX, Vector3 knockbackDirUp, Vector3 knockbackDirLeft)
    {
        float timer = 0;
        RB2D.velocity = new Vector2(0, 0);

        while (knockbackDur > timer)
        {
            timer += Time.deltaTime;

            RB2D.AddForce(new Vector3(knockbackDirLeft.x * knockBackPowerX, knockbackDirUp.y * knockBackPowerY, transform.position.z));

        }
        yield return 0;
    }
}
