using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllySoldierEntity : MonoBehaviour
{
    private List<GameObject> targets = new List<GameObject>();
    private GameObject mainTarget = null;
    private Vector3 mainTargetPosition => new Vector3(mainTarget.transform.position.x, transform.position.y, mainTarget.transform.position.z);
    private Vector3 idleDestination;
    private Vector3 spawnPosition;
    private BulletGenerator bg;
    private MovementUtils mu;

    public float Speed;
    public float TurningSpeed;
    public float WalkingDistance;
    public float NoticeRadius;
    public Transform FieldOfView;
    private Vector3 velocity;
    public float SmoothTime;
    public Gun Gun;
    public Transform ShootPoint;
    private Rigidbody recoilRB;

    private int LastShot;

    void Start()
    {
        spawnPosition = transform.position;
        bg = GameObject.FindGameObjectWithTag("GameManager").GetComponent<BulletGenerator>();
        mu = GameObject.FindGameObjectWithTag("GameManager").GetComponent<MovementUtils>();

        FieldOfView.GetComponent<SphereCollider>().radius = NoticeRadius;
        recoilRB = GetComponent<Rigidbody>();

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
    }
    
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        LastShot++;

        if(mainTarget == null)
        {
            if (idleDestination == Vector3.zero)
            {
                if ((transform.position - spawnPosition).magnitude > WalkingDistance)
                {
                    idleDestination = spawnPosition;
                }
                else
                {
                    idleDestination = transform.position + new Vector3(Random.Range(-2.0f, 2.0f), 0, Random.Range(-2.0f, 2.0f));

                    if(Vector3.Angle(transform.forward, idleDestination - transform.position) > 90)
                    {
                        idleDestination = transform.position - (idleDestination - transform.position);
                    }
                }
            }

            mu.RotateTowards(idleDestination, transform, TurningSpeed);

            var angle = Vector3.Angle(transform.forward, idleDestination - transform.position);

            if (angle <= 5)
            {
                mu.MoveInDirection(transform, idleDestination - transform.position, Speed, ref velocity, SmoothTime);

                if((transform.position - idleDestination).magnitude <= 0.25f)
                {
                    idleDestination = Vector3.zero;
                }
            }
        }
        else
        {
            mu.RotateTowards(mainTargetPosition, transform, TurningSpeed);

            var angle = Vector3.Angle(transform.forward, mainTargetPosition - transform.position);

            if (angle <= 5)
            {
                if((mainTargetPosition - transform.position).magnitude > NoticeRadius - 1)
                {
                    mu.MoveInDirection(transform, mainTargetPosition - transform.position, Speed, ref velocity, SmoothTime);
                }
                if (LastShot >= Gun.BaseShootingSpeed)
                {
                    LastShot = 0;
                    bg.Shoot(Gun, ShootPoint, recoilRB, new List<string> { "Player" });
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (Globals.EnemyTags.Contains(other.tag))
        {
            if (!targets.Contains(other.gameObject))
            {
                targets.Add(other.gameObject);
                if(Random.Range(0, 2) == 0 || targets.Count == 1)
                {
                    mainTarget = other.gameObject;
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (Globals.EnemyTags.Contains(other.tag))
        {
            if (targets.Contains(other.gameObject))
            {
                targets.Remove(other.gameObject);

                mainTarget = targets.Count > 0 ? targets[Random.Range(0, targets.Count)] : null;
            }
        }
    }
}
