using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    public Transform cam;

    public void LateUpdate()
    {
        transform.LookAt(transform.position + cam.forward);
    }
}
