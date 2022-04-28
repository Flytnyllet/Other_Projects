using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemeyGuy : MonoBehaviour
{

    public LayerMask enemyMask;
    Rigidbody2D RB2D;
    Transform myTrans;
    private PlayerController player;
    private GameMaster gm;

    float myWidth, myHeight;
    public float offsetLine;
    public float knockbackY;
    public float knockbackX;
    public float speed;

    public int playerDamageTakenInEditor;
    public int currentEnemyHealth;
    [Range(1,10)]
    public int enemyMaxHealth;

    bool knockbackTaken = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        gm = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<GameMaster>();

        myTrans = this.transform;
        RB2D = this.GetComponent<Rigidbody2D>();
        SpriteRenderer mySprite = this.GetComponent<SpriteRenderer>();
        myWidth = mySprite.bounds.extents.x;
        myHeight = mySprite.bounds.extents.y;

        currentEnemyHealth = enemyMaxHealth;
    }

    private void Update()
    {
        RB2D.freezeRotation = true;

        if (currentEnemyHealth > enemyMaxHealth)
        {
            currentEnemyHealth = enemyMaxHealth;
        }

        if (currentEnemyHealth <= 0)
        {
            Death();
        }
    }

    void FixedUpdate()
    {
        //Check if there is ground in front
        Vector2 lineCastPos = myTrans.position.toVector2() - myTrans.right.toVector2() * myWidth;

        //Debug.DrawLine(lineCastPos, lineCastPos + Vector2.down * myHeight * 2);

        bool isGrounded = Physics2D.Linecast(lineCastPos, lineCastPos + Vector2.down * myHeight * 2, enemyMask);
        RaycastHit2D hitObjects;
        hitObjects = Physics2D.Raycast(lineCastPos, -myTrans.right.toVector2(), offsetLine, enemyMask);

        //if (hitObjects.collider != null)
        //{
        //    Debug.DrawLine(lineCastPos, hitObjects.point, Color.green);
        //    Debug.Log(hitObjects.collider.name);
        //}

        if (!isGrounded || (hitObjects.collider != null && hitObjects.collider.tag != "Player" && hitObjects.collider.tag != "GroundCheck"))
        {
            Vector3 currRot = myTrans.eulerAngles;
            currRot.y += 180;
            myTrans.eulerAngles = currRot;
        }

        //Always move forward
        Vector2 myVel = RB2D.velocity;
        myVel.x = -myTrans.right.x * speed;
        RB2D.velocity = myVel;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            if (knockbackTaken == false)
            {
                player.TakeDamage(playerDamageTakenInEditor);

                StartCoroutine(player.Knockback(0.002f, knockbackY, knockbackX, Vector3.up, Vector3.left));
                knockbackTaken = true;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            knockbackTaken = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("GroundCheck"))
        {
            knockbackTaken = true;
            StartCoroutine(player.Knockback(0.002f, knockbackY, 0, Vector3.up, Vector3.left));
            player.Invulnerable = true;
            Death();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("GroundCheck"))
        {
            knockbackTaken = false;
        }
    }

    public void TakeDamage(int dmg)
    {
        currentEnemyHealth -= dmg;
    }

    void Death()
    {
        gm.points += 10;
        Destroy(transform.gameObject);
    }
}
