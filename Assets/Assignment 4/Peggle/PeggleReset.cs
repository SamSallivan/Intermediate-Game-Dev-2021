using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeggleReset : MonoBehaviour
{

    public PeggleBall mpb;
    public PeggleShooter mps;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == mpb.gameObject)
        {
            mps.ResetBall();
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
