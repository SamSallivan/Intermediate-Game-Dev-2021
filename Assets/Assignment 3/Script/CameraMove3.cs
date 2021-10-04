using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove3 : MonoBehaviour
{
    public Vector2 TargetPosition;

    public float TargetSize = 10;


    private void Update()
    {
        Vector3 target = new Vector3(TargetPosition.x, TargetPosition.y, -10);
        transform.position = Vector3.Lerp(transform.position, target, 1 * Time.deltaTime);

        GetComponent<Camera>().orthographicSize = Mathf.Lerp(GetComponent<Camera>().orthographicSize, TargetSize, 1 * Time.deltaTime);
    }
}
