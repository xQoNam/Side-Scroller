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
    float leftLimitX, rightLimitX, downLimitY, upLimitY;

    private void Awake()
    {
        setCameraLimits();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 desiredPosition = target.position + offset;
        desiredPosition = new Vector3(Mathf.Clamp(desiredPosition.x, leftLimitX, rightLimitX), Mathf.Clamp(desiredPosition.y, downLimitY, upLimitY), -10);
        Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, lerpSpeed);
        transform.position = smoothedPosition;
    }

    void setCameraLimits()
    {
        leftLimitX = limiters[0].transform.position.x + 15;
        rightLimitX = limiters[1].transform.position.x - 15;
        downLimitY = limiters[2].transform.position.y + 8.5f;
        upLimitY = limiters[3].transform.position.y - 8.5f;
    }
}
