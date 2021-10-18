using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hurtbox : MonoBehaviour
{
	public GameObject owner;
    public GameObject strawberryManager;
    public Health health;

    public bool hitted;
    public float hitCD;
    public Vector2 dir;

    public Rigidbody2D Rigidbody2D;

    private void Start()
    {
        strawberryManager = GameObject.Find("Strawberry Manager");
        health = owner.GetComponent<Health>();
    }

    private void Update()
    {
        if (hitted)
        {
            if (owner.name == "Madeline")
            {
                owner.GetComponent<PlayerMovement>().StartCoroutine(owner.GetComponent<PlayerMovement>().DisableMovement(0.25f));
            }
            else if (CompareTag("enemy"))
            {

            }

            StartCoroutine(hitRecovery());

            owner.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            owner.GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Sign(dir.x) * 20.0f, Mathf.Sign(dir.y+1) * 30.0f);

        }
    }

    IEnumerator hitRecovery()
    {
        if (owner.name == "Madeline")
        {
            Time.timeScale = 0.1f;
        }
        else
        {
            Time.timeScale = 0.5f;
        }

        hitted = false;
        Color tmp = owner.GetComponent<SpriteRenderer>().color;
        tmp.a = 0.25f;
        owner.GetComponent<SpriteRenderer>().color = tmp;
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
            owner.GetComponent<PlayerMovement>().Jump((Vector2.up * 1.5f), true);
        }

    }
}
