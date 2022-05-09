using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierEntity : Entity
{
    public float Speed;
    public float SmoothTime;
    public Transform ShootPoint;
    public Gun Gun;
    public Bullet BulletPrefab;
    public Transform BulletHolder;

    public bool DeactivateMovement = false;
    public bool DeactivateRotation = false;
    public bool DeactivateShooting = false;

    private Vector3 velocity;

    private int LastShot;
    private float EndReloadTime;
    private float CurrentReloadTime;

    private Rigidbody rb;
    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
