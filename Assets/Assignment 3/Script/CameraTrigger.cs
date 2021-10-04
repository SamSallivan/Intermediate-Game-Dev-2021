using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTrigger : MonoBehaviour
{
    public Vector2 CamPos;

    public float CamSize;

    public bool triggered = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && triggered == false)
        {
            Camera.main.GetComponent<CameraMove3>().TargetPosition = CamPos;
            Camera.main.GetComponent<CameraMove3>().TargetSize = CamSize;
            triggered = true;

            if (gameObject.name == "Trigger 6")
            {
                GameObject[] newPlayers = GameObject.FindGameObjectsWithTag("Player_Potential");
                for (int i = 0; i < newPlayers.Length; i++)
                {
                    newPlayers[i].gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
                }
            }
        }
    }
}
