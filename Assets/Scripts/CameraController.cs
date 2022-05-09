using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Vector3 CameraOffset;
    public float MaxDistanceFromPlayer;
    public float Smoothness;
    public float Speed;
    public float ZoomSpeed;

    public float MinZoomMagnitude;
    public float MaxZoomMagnitude; 

    public Transform Player;

    private Vector3 velocity;
    private Vector3 ZoomDirection;

    void Start()
    {
        ZoomDirection = -CameraOffset.normalized;
    }
    
    void Update()
    {
        // Following the player
        Vector3 correctedPlayerPosition = Player.position + CameraOffset - new Vector3(0, Player.position.y, 0); // This applies offset and player's own height to calculations
        
        float distance = Vector3.Distance(transform.position, correctedPlayerPosition);
        float distanceCoefficient = (distance * distance * 0.5f / MaxDistanceFromPlayer); // The farther player goes the faster camera approaches

        Vector3 directionToPlayer = (correctedPlayerPosition - transform.position).normalized;

        var newPosition = Vector3.SmoothDamp(transform.position, transform.position + directionToPlayer * Speed * distanceCoefficient, ref velocity, Smoothness);

        transform.position = newPosition;




        // Zooming to player
        var scrollDirection = Input.GetAxisRaw("Mouse ScrollWheel");

        if(scrollDirection != 0)
        {
            var newCameraOffset = CameraOffset + ZoomDirection * scrollDirection * ZoomSpeed;

            if(newCameraOffset.magnitude > MinZoomMagnitude && newCameraOffset.magnitude < MaxZoomMagnitude)
            {
                CameraOffset = newCameraOffset;
            }
        }
    }
}
