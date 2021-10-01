using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMove : MonoBehaviour
{
    private void Update()
    {
        transform.position = new Vector3((GameObject.Find("Main Camera").transform.position.x + 39f)*0.55f, transform.position.y, transform.position.z);

    }
}
