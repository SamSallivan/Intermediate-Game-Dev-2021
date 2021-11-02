using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MySimpleSystemRandom : MonoBehaviour
{
    public GameObject blockPrefab;
    public int seed = 0;
    System.Random r;

    void Start()
    {
        r = new System.Random();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnBlockObject();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

    }
    void SpawnBlockObject()
    {
        float x = (float)(r.NextDouble() * (8.0f - (-8.0f)) + (-8.0f));
        float y = (float)(r.NextDouble() * (5.0f - (-5.0f)) + (-5.0f));

        GameObject b = Instantiate(blockPrefab, new Vector3(x, y), Quaternion.identity);
    }
}
