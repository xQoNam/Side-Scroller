using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public float lerpSpeed;
    public Vector3 offset;
    private Vector3 velocity = Vector3.zero;
    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, lerpSpeed);
        transform.position = smoothedPosition;
    }
}
