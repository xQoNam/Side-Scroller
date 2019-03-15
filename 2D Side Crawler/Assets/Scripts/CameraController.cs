using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    [SerializeField]private GameObject[] limiters;
    public float lerpSpeed;
    public Vector3 offset;
    private Vector3 velocity = Vector3.zero;
    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 desiredPosition = target.position + offset;
        desiredPosition = new Vector3(Mathf.Clamp(desiredPosition.x, limiters[0].transform.position.x +15, limiters[1].transform.position.x - 15), Mathf.Clamp(desiredPosition.y, limiters[2].transform.position.y + 8.5f, limiters[3].transform.position.y - 8.5f), -10);
        Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, lerpSpeed);
        transform.position = smoothedPosition;
    }
}
