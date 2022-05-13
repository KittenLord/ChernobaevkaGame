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
    private BulletGenerator bg;
    private MovementUtils mu;

    private List<GameObject> targets;
    private List<GameObject> following;
    private GameObject mainTarget;
    private GameObject passiveTarget;


    void Start()
    {
        FieldOfView.radius = Range;
        targets = new List<GameObject>();
        following = new List<GameObject>();
        rb = GetComponent<Rigidbody>();
        bg = GameObject.FindGameObjectWithTag("GameManager").GetComponent<BulletGenerator>();
        mu = GameObject.FindGameObjectWithTag("GameManager").GetComponent<MovementUtils>();

        Gun = new Gun("Gun", 1,         // Ammo
                          false,        // Automatic
                AmmoType.Bullet,        // Ammo Type
                             15,         // Damage
                             100,        // Shooting Speed
                            150,         // Recoil
                             1,         // Max Ammo
                            10,         // Accuracy
                             1,        // Shot Count
                             0,          // Reload Time (in seconds)
                             50         // Bullet Speed
        );

        passiveTarget = GameObject.FindGameObjectWithTag("Base");
    }

    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        LastShot++;

        if (mainTarget != null)
        {
            mu.RotateTowards(mainTarget, transform, TurningSpeed);

            var angle = Vector3.Angle(transform.forward, mainTarget.transform.position - transform.position);

            if (angle <= 5)
            {
                if(LastShot >= Gun.BaseShootingSpeed)
                {
                    LastShot = 0;
                    bg.Shoot(Gun, ShootPoint, BulletHolder, rb, new List<string> { "Soldier" });
                }
            }
        }
        else
        {
            mu.RotateTowards(passiveTarget, transform, TurningSpeed);

            var angle = Vector3.Angle(transform.forward, passiveTarget.transform.position - transform.position);

            if (angle <= 5)
            {
                var direction = transform.forward;

                if(following.Count != 0)
                {
                    //GameObject closestFollowing = following[0];
                    //foreach (var f in following)
                    //{
                    //    if((f.transform.position - transform.position).magnitude < (closestFollowing.transform.position - transform.position).magnitude)
                    //    {
                    //        closestFollowing = f;
                    //    }
                    //}



                    //direction += dir;

                    List<GameObject> groupMembers = new List<GameObject>(following);
                    groupMembers.Add(gameObject);

                    Vector3 average = Vector3.zero;
                    foreach (var v in groupMembers)
                    {
                        average += v.transform.position;
                    }
                    average /= (float)groupMembers.Count;

                    var dir = (average - transform.position).normalized;

                    if ((average - transform.position).magnitude < 1.5) dir *= -1;

                    direction += dir;
                }

                mu.MoveInDirection(transform, direction, Speed, ref velocity, SmoothTime);
            }

        }
        
    }

    private void SetTarget()
    {
        if (targets.Count == 0)
        {
            mainTarget = null;
            return;
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

            if(result >= current && result < current + probability)
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

        if(other.tag == "Soldier")
        {
            if (!following.Contains(other.gameObject))
            {
                following.Add(other.gameObject);
            }
        }
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

        if (other.tag == "Soldier")
        {
            if (following.Contains(other.gameObject))
            {
                following.Remove(other.gameObject);
            }
        }
    }
}
