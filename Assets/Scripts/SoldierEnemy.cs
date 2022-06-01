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

        mu.RotateTowards(mainTarget, transform, TurningSpeed);

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

            mu.MoveInDirection(transform, direction.normalized, Speed, ref velocity, SmoothTime);

            if (LastShot >= Gun.BaseShootingSpeed && (mainTargetPosition - transform.position).magnitude < NoticeRadius)
            {
                LastShot = 0;
                bg.Shoot(Gun, ShootPoint, recoilRB, new List<string> { "Soldier" });
            }
        }

    }


    #region Old AI
    //private void FixedUpdate()
    //{
    //    LastShot++;
    //
    //    if (mainTarget != null)
    //    {
    //        mu.RotateTowards(mainTarget, transform, TurningSpeed);
    //
    //        var angle = Vector3.Angle(transform.forward, mainTarget.transform.position - transform.position);
    //
    //        if (angle <= 5)
    //        {
    //            if(LastShot >= Gun.BaseShootingSpeed)
    //            {
    //                LastShot = 0;
    //                bg.Shoot(Gun, ShootPoint, rb, new List<string> { "Soldier" });
    //            }
    //        }
    //    }
    //    else
    //    {
    //        mu.RotateTowards(passiveTarget, transform, TurningSpeed);
    //
    //        var angle = Vector3.Angle(transform.forward, passiveTarget.transform.position - transform.position);
    //
    //        if (angle <= 5)
    //        {
    //            var direction = transform.forward;
    //
    //            if(following.Count != 0)
    //            {
    //                var dirToGroup = GetAverageGroupPosition(following) - transform.position;
    //
    //                if (dirToGroup.magnitude < 1.5) dirToGroup *= -1;
    //
    //                direction += dirToGroup.normalized;
    //            }
    //
    //            mu.MoveInDirection(transform, direction, Speed, ref velocity, SmoothTime);
    //        }
    //
    //    }
    //    
    //}
    #endregion





}
