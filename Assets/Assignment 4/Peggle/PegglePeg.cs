using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PegglePeg : MonoBehaviour
{

    public Color newColor = Color.green;

    public void OnCollisionEnter2D(Collision2D collision)
    {
        //Destroy(gameObject);
        GetComponent<Collider2D>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;
        //GetComponent<SpriteRenderer>().color = newColor;
    }

}
