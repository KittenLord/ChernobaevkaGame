using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierEnemy : BaseEnemy
{
    public Gun Gun;                     // Wanted to put these two categories (grouping and shooting) in separate interfaces of some sort, but goddamn unity's inspector 
    public Transform ShootPoint;        // will not fucking allow me to see properties in it, and will NOT do the whole [SerializeField] bullshit
    public Transform BulletHolder;
    private int LastShot;


    public float GroupingMinDistance;
    public float GroupingMaxDistance; 

    void Start()
    {

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


    private void FixedUpdate()
    {
        LastShot++;

        if(mainTarget == null)
        {
            mainTarget = baseTarget.gameObject;
        }

        if(!DeactivateRotation) mu.RotateTowards(mainTarget.transform.position, transform, TurningSpeed);

        var angle = Vector3.Angle(transform.forward, mainTargetPosition - transform.position);

        if (angle <= 5)
        {
            var direction = Vector3.zero;

            if (following.Count != 0)
            {
                var list = new List<GameObject>(following);
                list.Add(gameObject);

                var dirToGroup = Globals.GetAverageGroupPosition(list) - transform.position;

                if (dirToGroup.magnitude < GroupingMinDistance) dirToGroup *= -1;

                if (dirToGroup.magnitude > GroupingMinDistance && dirToGroup.magnitude < GroupingMaxDistance) dirToGroup = Vector3.zero;

                direction += dirToGroup.normalized * 0.25f;
            }

            if((mainTargetPosition - transform.position).magnitude > ClosestDistance)
            {
                direction += transform.forward;
            }

            if (!DeactivateMovement) mu.MoveInDirection(transform, direction.normalized, Speed, ref velocity, SmoothTime);

            if (LastShot >= Gun.BaseShootingSpeed && (mainTargetPosition - transform.position).magnitude < NoticeRadius && !DeactivateShooting)
            {
                LastShot = 0;
                bg.Shoot(Gun, ShootPoint, recoilRB, new List<string> { "Soldier" });
            }
        }

    }
}
