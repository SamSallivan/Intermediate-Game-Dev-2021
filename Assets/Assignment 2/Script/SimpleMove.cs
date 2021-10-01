using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMove : MonoBehaviour
{

    [SerializeField]
    private float speed = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void MoveObject()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(Vector2.left * Time.deltaTime * speed);
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(Vector2.right * Time.deltaTime * speed);
        }
    }

    // Update is called once per frame
    void Update()
    {
        MoveObject();
    }
}
