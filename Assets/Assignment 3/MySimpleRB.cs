using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MySimpleRB : MonoBehaviour {

    Rigidbody2D rb;
    bool moveLeft;
    bool moveRight;
    bool moveInitiate;
    public float power = 1.0f;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        
    }

    void CheckInput() { 

        if (Input.GetKey(KeyCode.LeftArrow)) {
            moveLeft = true; 
        }
        else {
            moveLeft = false;
        }
        moveRight = Input.GetKey(KeyCode.RightArrow);
        moveInitiate = Input.GetKey(KeyCode.Space);
    }

    private void Update() {
        CheckInput();
    }
    private void FixedUpdate() {

        if (moveLeft)
            rb.AddForce(Vector2.left * power);

        if (moveRight)
            rb.AddForce(Vector2.right * power);

        if (moveInitiate)
        {
            rb.AddForce(Vector2.right * power * 10);
            rb.AddForce(Vector2.up * power * 10);
        }




        //rb.AddForce(new Vector2(-1.0f, 0.0f)); //This line is the same as the one before it

    }
}
