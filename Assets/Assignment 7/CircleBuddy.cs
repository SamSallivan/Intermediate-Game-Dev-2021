using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleBuddy : MonoBehaviour
{
    public float speed = 1;
    Vector2 nextLocation = new Vector2();
    GridSystem gridSystem;
    SpriteRenderer spriteRenderer;

    bool isActive;
    float pauseTime = 1.0f;


    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

    }


    void Start()
    {
        gridSystem = GameObject.FindGameObjectWithTag("Grid_System").GetComponent<GridSystem>();
        isActive = true;
        StartCoroutine(MoveToLocation());
    }

    IEnumerator MoveToLocation()
    {
        while (isActive)
        {
            float t = 0.0f;
            nextLocation = gridSystem.GetRandomLocation();
            Vector2 startLocation = transform.position;

            spriteRenderer.color = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));

            while (t < 1.0f)
            {
                t += Time.deltaTime * speed * gridSystem.buddySpeed;
                transform.position = Vector2.Lerp(startLocation, nextLocation, t);
                yield return null;

            }


            yield return new WaitForSeconds(gridSystem.pauseTime);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
