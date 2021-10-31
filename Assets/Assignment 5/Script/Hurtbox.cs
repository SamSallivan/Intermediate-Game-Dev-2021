using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hurtbox : MonoBehaviour
{
	public GameObject owner;
    public GameObject hitbox;
    public GameObject strawberryManager;
    public Health health;
    //public Animator animator;
    //public Animation animationDamaged;

    public bool hitted;
    public bool isStunned;
    public float hitCD;
    public float hitStun = 0.5f;
    public Vector2 dir;

    public Rigidbody2D Rigidbody2D;

    private void Start()
    {
        owner = transform.parent.gameObject;
        strawberryManager = GameObject.Find("Strawberry Manager");
        health = owner.GetComponent<Health>();
        //animator = owner.GetComponent<Animator>();
    }

    private void Update()
    {
        if (hitted)
        {
            if (owner.name == "Madeline")
            {
                owner.GetComponent<PlayerMovement>().StartCoroutine(owner.GetComponent<PlayerMovement>().DisableMovement(0.25f));
                owner.GetComponent<PlayerMovement>().side = (int)Mathf.Sign(-dir.x);

            }
            //else if (CompareTag("enemy"))
            //{

            //}
            Debug.Log("Hit");
            StartCoroutine(Stunned(hitStun));

            StartCoroutine(hitRecovery());

            if (hitbox.name == "Fall Collider")
                return;

            owner.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            owner.GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Sign(dir.x) * 15.0f, Mathf.Sign(dir.y+1) * 20.0f);

        }
    }

    IEnumerator Stunned(float time)
    {
        isStunned = true;
        yield return new WaitForSeconds(time);
        isStunned = false;
    }

    IEnumerator hitRecovery()
    {
        hitted = false;
        Color tmp = owner.GetComponent<SpriteRenderer>().color;

        if (owner.name == "Madeline")
        {
            Time.timeScale = 0.1f;
            owner.GetComponent<PlayerMovement>().Animation("Player_Damaged");
        }
        if (owner.name == "Spider")
        {
            Time.timeScale = 0.5f;
            if (!owner.GetComponent <Animator>().GetCurrentAnimatorStateInfo(0).IsName("Spider_Damaged"))
            {
                owner.GetComponent<Animator>().Play("Spider_Damaged", 0, 0);
                //Destroy(owner);
            }
        }
        if (owner.name == "Flower")
        {
            Time.timeScale = 0.5f;
            if (!owner.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Flower_Damaged"))
            {
                owner.GetComponent<Animator>().Play("Flower_Damaged", 0, 0);
                //Destroy(owner);
            }
        }
        else
        {
            Time.timeScale = 0.5f;
            tmp.a = 0.25f;
            owner.GetComponent<SpriteRenderer>().color = tmp;
        }


        health.invincible = true;

        yield return new WaitForSeconds(0.05f);

        Time.timeScale = 1.0f;

        yield return new WaitForSeconds(hitCD);

        if (!owner.GetComponent<Health>().dead)
        {
            tmp.a = 1.0f;
            owner.GetComponent<SpriteRenderer>().color = tmp;
        }

        health.invincible = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (owner.name == "Madeline" && collision.gameObject.tag == "Strawberry")
        {
            Destroy(collision.gameObject);
            strawberryManager.GetComponent<StrawberryManage>().strawberries++;
        }

        if (collision.gameObject.name == "Fall Collider")
        {
            health.dead = true;
        }

        if (collision.gameObject.name == "Jump Pad")
        {
            owner.GetComponent<PlayerMovement>().VariableJump = false;
            owner.GetComponent<Rigidbody2D>().gravityScale = owner.GetComponent<PlayerMovement>().gravityScaler;
            owner.GetComponent<PlayerMovement>().Jump((Vector2.up), true);
        }

    }
}
