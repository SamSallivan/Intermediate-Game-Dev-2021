using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerRespawn : MonoBehaviour
{
    Rigidbody2D myBody;
    SpriteRenderer myRenderer;
    Health health;

    public Vector2 CheckPoint = new Vector2(-13, -5);

    void Start()
    {
        myRenderer = gameObject.GetComponent<SpriteRenderer>();
        myBody = gameObject.GetComponent<Rigidbody2D>();
        health = gameObject.GetComponent<Health>();
    }
    void Update()
    {
        if (health.dead == true)
        {
            StartCoroutine(Respawn());
        }

    }
    IEnumerator Respawn()
    {
        Color tmp = GetComponent<SpriteRenderer>().color;
        tmp.a = 0.25f;
        GetComponent<SpriteRenderer>().color = tmp;
        yield return new WaitForSeconds(0.75f);
        transform.position = CheckPoint;
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        health.invincible = true;
        StopCoroutine(GetComponent<PlayerMovement>().DisableMovement(0));
        StartCoroutine(GetComponent<PlayerMovement>().DisableMovement(0.5f));
        health.dead = false;
        health.HealthPoint = health.maxHealthPoint;
        yield return new WaitForSeconds(0.5f);
        yield return new WaitForSeconds(1.0f);
        tmp.a = 1.0f;
        GetComponent<SpriteRenderer>().color = tmp;
        health.invincible = false;
    }

}
