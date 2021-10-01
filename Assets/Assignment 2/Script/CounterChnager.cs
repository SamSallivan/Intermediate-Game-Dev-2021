using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CounterChnager : MonoBehaviour
{

    public enum Stages { Increase, Decrease }
    public Stages myStage;

    public TMP_Text counterText;
    public int counter = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void EnumChange()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            switch (myStage)
            {
                case Stages.Increase:
                    counter++;
                    break;

                case Stages.Decrease:
                    counter--;
                    break;

            }
            counterText.text = counter.ToString();
        }

        if (Input.GetKey(KeyCode.S))
        {
            switch (myStage)
            {
                case Stages.Increase:
                    myStage = Stages.Decrease;
                    break;

                case Stages.Decrease:
                    myStage = Stages.Increase;
                    break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        EnumChange();
    }
}
