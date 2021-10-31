using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTrigger4 : MonoBehaviour
{
    public Vector2 CamRangeX;
    public Vector2 CamRangeY;
    public float CamSize;
    public bool Disable = true;
    public Vector3 RespawnOffset = new Vector3(-1, 0, 0);
    //public float CamLerp;

    private void Update()
    {
        if (Camera.main.GetComponent<CameraMove4>().Lerp == 10)
        {
            StartCoroutine(LerpBack());
        }
    }



    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (Camera.main.GetComponent<CameraMove4>().TargetRangeX == CamRangeX && Camera.main.GetComponent<CameraMove4>().TargetRangeY == CamRangeY)
            {
                //Camera.main.GetComponent<CameraMove4>().Lerp = 3000;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (Camera.main.GetComponent<CameraMove4>().TargetRangeX != CamRangeX || Camera.main.GetComponent<CameraMove4>().TargetRangeY != CamRangeY)
            {
                collision.GetComponent<PlayerRespawn>().CheckPoint = transform.position + RespawnOffset;
                //collision.transform.position = new Vector2(transform.position.x + RespawnOffset.x/2, collision.transform.position.y);
                Camera.main.GetComponent<CameraMove4>().TargetRangeX = CamRangeX;
                Camera.main.GetComponent<CameraMove4>().TargetRangeY = CamRangeY;
                Camera.main.GetComponent<CameraMove4>().TargetSize = CamSize;
                Camera.main.GetComponent<CameraMove4>().Lerp = 10;

                if (Disable) {
                    StopCoroutine(GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().DisableMovement(0));
                    StartCoroutine(GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().DisableMovement(0.5f));
                }
                
            }
        }
    }

    IEnumerator LerpBack()
    {
        yield return new WaitForSeconds(1f);

        if (Camera.main.GetComponent<CameraMove4>().TargetRangeX == CamRangeX && Camera.main.GetComponent<CameraMove4>().TargetRangeY == CamRangeY)
        {
            Camera.main.GetComponent<CameraMove4>().Lerp = 3000;
        }
    }
}
