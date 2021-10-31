using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    public Vector2 rangeX;
    public Camera camera;
    public GameObject[] backgrounds;
    void Start()
    {
        camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {

        float range1 = Mathf.Max(0, rangeX.y - rangeX.x - 62.8f);
        float range2 = rangeX.y - rangeX.x - 32;

        if (range2 > 0)
        {
            float ratio = range1 / range2;


            float backPos = camera.transform.position.x;

            float frontPos = rangeX.x + 31.4f + (camera.transform.position.x - rangeX.x - 16) * ratio;

            //Debug.Log("back " + backPos);
            //Debug.Log("front " + frontPos);
            //Debug.Log("ratio " + ratio);

            for (int i = 0; i < backgrounds.Length; i++)
            {
                float ratioPos = (frontPos - backPos) * i / (backgrounds.Length - 1);

                //Debug.Log("ratioPos " + ratioPos); 

                backgrounds[i].transform.position = new Vector3(backPos + ratioPos, backgrounds[i].transform.position.y, 0);
            }
        }


    }
}
