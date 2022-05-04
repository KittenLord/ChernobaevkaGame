using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float Speed;
    public float SmoothTime;
    private Vector3 velocity;
    public int PlaneLayer;



    void Start()
    {
        
    }



    void Update()
    {
        // Movement Detection
        var direction = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;

        var nextPosition = Vector3.SmoothDamp(transform.position, transform.position + direction * Speed, ref velocity, SmoothTime);

        transform.position = nextPosition;



        // Rotation Detection
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitinfo;
        Physics.Raycast(ray, out hitinfo, 9999, 1 << PlaneLayer); // 1 << PlaneLayer makes it only react to the object with layer PlaneLayer
        var hitPoint = new Vector3(hitinfo.point.x, transform.position.y, hitinfo.point.z); // Make the target point the same height, as the player, so no falling down will occur

        transform.LookAt(hitPoint);
    }
}