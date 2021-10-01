using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{

    private void Update() {
        float target_x;

        target_x = Mathf.Lerp(transform.position.x, GameObject.Find("Mae").transform.position.x, 10*Time.deltaTime);

        target_x = Mathf.Clamp(target_x, -4.65f, 52.65f);
        transform.position = new Vector3(target_x, transform.position.y, transform.position.z);

    }
}
