using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleCollision : MonoBehaviour
{
    private BoxCollider2D collider;

    private void Awake()
    {
        collider = GetComponent<BoxCollider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Player_Potential"))
        {
            GetComponent<SpriteRenderer>().color = new Color(1, 0.6f, 0); //new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
        }
        else
        {
            //collision.gameObject.SetActive(false);
        }

        //Debug.Log("A collision has happened!!", gameObject);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //Debug.Log("A collision has happened again!!!");
    }

    private void OnTriggerStay2D(Collider2D collision)
    {

    }
}
