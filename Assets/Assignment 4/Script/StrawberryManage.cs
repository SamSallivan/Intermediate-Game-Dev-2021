using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StrawberryManage : MonoBehaviour
{
    public int strawberries = 0;

    public TMP_Text text;

    void Start()
    {
        
    }

    void Update()
    {
        displayNum();

        if (strawberries >= 5)
        {
            GameObject.Find("Theo 0/5").GetComponent<DialogueTrigger>().enabled = false;
            GameObject.Find("Theo 5/5").GetComponent<DialogueTrigger>().enabled = true;
            GameObject.Find("Door").GetComponent<SpriteRenderer>().enabled = false;
            GameObject.Find("Door").GetComponent<BoxCollider2D>().enabled = false;
        }
    }
    void displayNum()
    {
        text.text = ("  " + strawberries + " / 5 Strawberries");
    }
}
