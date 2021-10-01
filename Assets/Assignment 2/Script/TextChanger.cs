using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextChanger : MonoBehaviour
{
    public enum Stages { StageOne, StageTwo, StageThree }
    public Stages myStage = Stages.StageOne;
    public int counter = 0;

    [SerializeField]
    private TMP_Text myText;
    [SerializeField]
    private string newText = "This is not a NEW TEXT :3";

    private void Awake()
    {
        myText = GetComponent<TMP_Text>();
    }

    // Start is called before the first frame update
    void Start()
    {
        //myText.text = "This is some text. :3";
        myText.text = newText;
    }

    void EnumChnage()
    {
        /*
        swith(myStage){ 
            case Stages.StageOne:
                myText.text = "Stage One!!";
                myStage = Stages.StageTwo;
                break; 

            case Stages.StageTwo:
                myText.text = "Stage Two!!";
                myStage = Stages.StageThree;
                break; 

            case Stages.StageThree:
                myText.text = "Stage Three!!";
                myStage = Stages.StageOne;
                break;
        } 
        */
        if (myStage == Stages.StageOne)
        {
            myText.text = "Stage One!!";
            myStage = Stages.StageTwo;
        }
        else if (myStage == Stages.StageTwo)
        {
            myText.text = "Stage Two!!";
            myStage = Stages.StageThree;
        }
        else if (myStage == Stages.StageThree)
        {
            myText.text = "Stage Three!!";
            myStage = Stages.StageOne;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.S))
        {
            EnumChnage();
        }

        if ( Input.GetKey(KeyCode.Space))
        {
            myText.text = "Space Pressed :3";
        }
    }
}
