using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform follow;

    private void Start()
    {
        this.transform.position.Set(1.7f, 0f, -5f);
    }

    private void LateUpdate()
    {
        this.transform.position = new Vector3((follow.position.x + 2.8f), (follow.position.y + 2.3f), this.transform.position.z);
    }
}
