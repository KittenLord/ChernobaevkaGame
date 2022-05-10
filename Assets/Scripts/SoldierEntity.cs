using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierEntity : Entity
{
    public float Speed;
    public float TurningSpeed;
    public float SmoothTime;
    public Transform ShootPoint;
    public Gun Gun;
    public Bullet BulletPrefab;
    public Transform BulletHolder;
    public SphereCollider FieldOfView;
    public float Range;

    public bool DeactivateMovement = false;
    public bool DeactivateRotation = false;
    public bool DeactivateShooting = false;

    private Vector3 velocity;

    private int LastShot;

    private Rigidbody rb;

    private List<GameObject> targets;
    private GameObject mainTarget;


    void Start()
    {
        FieldOfView.radius = Range;
        targets = new List<GameObject>();
        rb = GetComponent<Rigidbody>();

        Gun = new Gun("Gun", 1,         // Ammo
                          false,        // Automatic
                             15,         // Damage
                             100,        // Shooting Speed
                            30,         // Recoil
                             1,         // Max Ammo
                            10,         // Accuracy
                             1,        // Shot Count
                             0          // Reload Time (in seconds)
        );
    }

    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        LastShot++;

        if (mainTarget != null)
        {
            var direction = AngleDir(transform.forward, (mainTarget.transform.position - transform.position).normalized);

            transform.Rotate(new Vector3(0, direction * TurningSpeed, 0));

            if (Vector3.Angle(transform.forward, mainTarget.transform.position - transform.position) <= 20)
            {
                if(LastShot >= Gun.BaseShootingSpeed)
                {
                    LastShot = 0;
                    Shoot();
                }
            }
        }
    }


    public void Shoot()
    {
        for (int i = 0; i < Gun.ShotCount; i++)
            GenerateBullet();

        rb.AddForce(-transform.forward * Gun.Recoil);
    }

    public void GenerateBullet()
    {
        var bullet = Instantiate<Bullet>(BulletPrefab, ShootPoint.position, Quaternion.identity, BulletHolder);

        var direction = ShootPoint.transform.forward;

        float min = -Gun.Accuracy / 2.0f;
        float max = Gun.Accuracy / 2.0f;

        direction = Quaternion.Euler(new Vector3(Random.Range(min, max) / 10.0f, Random.Range(min, max), 0)) * direction;

        bullet.SetBullet(Gun, direction);
    }

    private void SetTarget()
    {
        if (targets.Count == 0)
        {
            mainTarget = null;
        }

        int probabilityPool = 0;
        foreach (var t in targets)
        {
            Globals.GetTargetPriority(t.tag);
        }

        int result = Random.Range(0, probabilityPool);

        int current = 0;
        foreach (var t in targets)
        {
            var probability = Globals.GetTargetPriority(t.tag);

            if(probabilityPool >= current && probabilityPool < probability)
            {
                mainTarget = t;
                return;
            }

            current = probability;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (Globals.EnemyTargetTags.Contains(other.tag))
        {
            if (!targets.Contains(other.gameObject))
            {
                targets.Add(other.gameObject);
                SetTarget();
            }
        }
    }

    public static float AngleDir(Vector3 A, Vector3 B)
    {
        return -A.x * B.z + A.z * B.x;
    }

    private void OnTriggerExit(Collider other)
    {
        if (Globals.EnemyTargetTags.Contains(other.tag))
        {
            if (targets.Contains(other.gameObject))
            {
                targets.Remove(other.gameObject);
                SetTarget();
            }
        }
    }
}
