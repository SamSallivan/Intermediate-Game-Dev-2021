using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeggleShooter : MonoBehaviour
{

    public Rigidbody2D ball;
    public Vector3 ballStartPos;

    void Start()
    {
        ballStartPos = ball.transform.localPosition;
    }

    public void ResetBall()
    {
        ball.velocity = Vector2.zero;
        ball.angularVelocity = 0;
        ball.simulated = false;

        ball.transform.SetParent(transform, true);
        ball.transform.localPosition = ballStartPos;
        ball.transform.localRotation = Quaternion.Euler(Vector3.zero);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
