using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed = 4;
    [SerializeField] private Sprite[] sprites;
    private int sprite_Index = 0;
    private float counter;

    // Update is called once per frame
    void Update() {

        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector2.left * Time.deltaTime * speed);
            transform.localScale = new Vector3(-1,1,1);
            counter += Time.deltaTime;
        }
        else if(Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * Time.deltaTime * speed);
            transform.localScale = new Vector3(1, 1, 1);
            counter += Time.deltaTime;
        }
        else
        {
            counter = 0;
            sprite_Index = 0;
        }

        if (counter >= 0.2)
        {
            counter = 0;
            if (sprite_Index != 7)
                sprite_Index++;
            else
                sprite_Index += 2;
            sprite_Index %= sprites.Length;
        }

            transform.position = new Vector3(Mathf.Clamp(transform.position.x, -10, 58), transform.position.y);

            GetComponent<SpriteRenderer>().sprite = sprites[sprite_Index];
        }
        
}
