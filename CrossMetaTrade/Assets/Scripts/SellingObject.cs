using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellingObject : MonoBehaviour
{
    Camera cam;

    void Update()
    {
        if (cam == null)
        {
            cam = FindObjectOfType<Camera>();
        }
        if (cam == null)
        {
            return;
        }

        transform.LookAt(cam.transform);
    }
}
