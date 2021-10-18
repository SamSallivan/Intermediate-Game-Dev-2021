using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy01Movement : MonoBehaviour
{
    public Health health;
    public Rigidbody2D rb;
    public SpriteRenderer myRenderer;
    public GameObject hitbox;
    public GameObject hurtbox;
    public LayerMask collisionLayer;
    public LayerMask playerLayer;

    public float initialX;
    public float distanceX;
    public float maxDistanceX;
    public float walkSpeed = 10f;
    public float runSpeed = 20f;
    public float hitStun = 0.5f;
    public int side = 1;
    public Vector2 bottomOffset, rightOffset1, rightOffset2, leftOffset1, leftOffset2;
    public float collisionRadius = 0.25f;


    public bool isAttacking;
    public bool isStunned;
    public bool onGround;

    void Start()
    {
        health = GetComponent<Health>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        myRenderer = gameObject.GetComponent<SpriteRenderer>();

        initialX = gameObject.transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        onGround = Physics2D.OverlapCircle((Vector2)transform.position + bottomOffset + new Vector2(0.75f, 0), collisionRadius, collisionLayer)
            || Physics2D.OverlapCircle((Vector2)transform.position + bottomOffset + new Vector2(-0.75f, 0), collisionRadius, collisionLayer);

        RaycastHit2D detection = Physics2D.Raycast(transform.position, new Vector2(side, 0f), 100f, playerLayer);
        if (detection.collider)
        {
                isAttacking = true;
        }
        else
        {
            isAttacking = false;
        } 

        if (hurtbox.GetComponent<Hurtbox>().hitted)
        {
            StartCoroutine(Stunned(hitStun));
        }

        if (!isStunned)
        {
            if (!isAttacking)
            {
                distanceX = Mathf.Abs(gameObject.transform.position.x - initialX);
                if ((distanceX > maxDistanceX) && (Mathf.Sign(gameObject.transform.position.x - initialX) == Mathf.Sign(side))) //speed and direction consistant
                {
                    side *= -1;
                }

                rb.velocity = new Vector3(side * walkSpeed, rb.velocity.y);
            }
            else
            {
                //side = Mathf.Sign(gameObject.transform.position.x - GameObject.FindGameObjectsWithTag("Player").transform.position.x);
                rb.velocity = new Vector3(side * runSpeed, rb.velocity.y);
            }
        }
            


    }

    public IEnumerator Stunned(float time)
    {
        isStunned = true;
        yield return new WaitForSeconds(time);
        isStunned = false;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere((Vector2)transform.position + bottomOffset + new Vector2(0.75f, 0), collisionRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + bottomOffset + new Vector2(-0.75f, 0), collisionRadius);


        RaycastHit2D detection = Physics2D.Raycast(transform.position, new Vector2(side, 0f), 20f, playerLayer);
        if (detection.collider)
        {
            Debug.DrawRay(transform.position, new Vector2(side, 0f) * 20f, Color.red);
        }
        else
        {
            Debug.DrawRay(transform.position, new Vector2(side, 0f) * 20f, Color.blue);
        }
    }
}
