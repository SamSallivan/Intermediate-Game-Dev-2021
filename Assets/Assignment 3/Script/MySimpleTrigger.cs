using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MySimpleTrigger : MonoBehaviour {

    private BoxCollider2D bc;

    private void Awake() {
        bc = GetComponent<BoxCollider2D>();
        bc.isTrigger = true;
    }



    private void OnTriggerEnter2D(Collider2D collision) {

        if (collision.CompareTag("Player")) {
            collision.GetComponent<SpriteRenderer>().color = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
        }
        else {
            collision.gameObject.SetActive(false);
        }

        Debug.Log("A collision has happened!!", gameObject);
    }

    private void OnTriggerExit2D(Collider2D collision) {
        Debug.Log("A collision has happened again!!!");
    }

    private void OnTriggerStay2D(Collider2D collision) {
        
    }
}
