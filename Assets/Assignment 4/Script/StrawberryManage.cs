using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StrawberryManage : MonoBehaviour
{
    public int strawberries = 0;

    public TMP_Text text;
    public TMP_Text health;

    void Start()
    {
        
    }

    void Update()
    {
        displayNum();

        if (strawberries >= 5)
        {
            GameObject.Find("Theo 0/5").GetComponent<SpriteRenderer>().enabled = false;
            GameObject.Find("Theo 0/5").GetComponent<DialogueTrigger>().enabled = false;
            GameObject.Find("Theo 5/5").GetComponent<SpriteRenderer>().enabled = true;
            GameObject.Find("Theo 5/5").GetComponent<DialogueTrigger>().enabled = true;
            GameObject.Find("Door 5/5").GetComponent<SpriteRenderer>().enabled = false;
            GameObject.Find("Door 5/5").GetComponent<BoxCollider2D>().enabled = false;
        }
    }
    void displayNum()
    {
        text.text = ("  " + strawberries + " / 5 Batteries");
        health.text = ("  " + GameObject.FindGameObjectWithTag("Player").GetComponent<Health>().HealthPoint + " / 10 HP");
    }
}
