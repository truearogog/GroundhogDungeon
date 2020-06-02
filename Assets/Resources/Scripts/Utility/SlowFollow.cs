using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowFollow : MonoBehaviour
{
    public Transform followTransform;
    public float followSpeed;

    void Start()
    {
        transform.position = followTransform.position;
    }

    void FixedUpdate()
    {
        if (followTransform)
        {
            transform.position = Vector3.Lerp(transform.position, followTransform.position, followSpeed);
        }
    }
}
