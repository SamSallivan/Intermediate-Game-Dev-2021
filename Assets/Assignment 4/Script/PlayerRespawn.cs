using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerRespawn : MonoBehaviour
{
    Rigidbody2D myBody;
    SpriteRenderer myRenderer;
    GameObject Manager;

    public bool dead = false;
    public Vector2 CheckPoint = new Vector2(-13, -5);

    void Start()
    {
        myRenderer = gameObject.GetComponent<SpriteRenderer>();
        myBody = gameObject.GetComponent<Rigidbody2D>();
        Manager = GameObject.Find("Strawberry Manager");
    }
    void Update()
    {

        if (dead == true)
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            transform.position = CheckPoint;
            StopCoroutine(GetComponent<PlayerMovement>().DisableMovement(0));
            StartCoroutine(GetComponent<PlayerMovement>().DisableMovement(0.5f));
            dead = false;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Strawberry")
        {
            Destroy(collision.gameObject);
            Manager.GetComponent<StrawberryManage>().strawberries++;
        }

        if (collision.gameObject.name == "Fall Collider")
        {
            dead = true;
        }

        if (collision.gameObject.name == "Jump Pad")
        {
            GetComponent<PlayerMovement>().VariableJump = false;
            GetComponent<PlayerMovement>().Jump((Vector2.up * 1.5f), true);
        }

    }

}
