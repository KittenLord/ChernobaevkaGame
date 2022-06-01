using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankEnemy : BaseEnemy
{
    public Gun Gun;
    public Transform ShootPoint;
    public Transform BulletHolder;
    private int LastShot;

    public Transform Body;
    public Transform Turret;
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
        Turret.position = new Vector3(Body.position.x, Turret.position.y, Body.position.z);

        LastShot++;

        if (mainTarget == null)
        {
            mainTarget = baseTarget.gameObject;
        }

        mu.RotateTowards(mainTarget, Turret, TurningSpeed);


        var angle = Vector3.Angle(Turret.forward, mainTargetPosition - Turret.position);

        if (angle <= 5)
        {
            if (LastShot >= Gun.BaseShootingSpeed && (mainTargetPosition - transform.position).magnitude < NoticeRadius)
            {
                LastShot = 0;
                bg.Shoot(Gun, ShootPoint, recoilRB, new List<string> { "Tank" });
            }
        }





        if ((mainTarget.transform.position - transform.position).magnitude > ClosestDistance)
        {
            mu.RotateTowards(mainTarget, transform, TurningSpeed);

            var a = Vector3.Angle(transform.forward, mainTargetPosition - transform.position);

            if(a > 90)
            {
                transform.rotation *= Quaternion.Euler(0, 180, 0);
                Turret.rotation *= Quaternion.Euler(0, 180, 0);
            }

            if (a <= 60)
            {
                mu.MoveInDirection(transform, transform.forward, Speed, ref velocity, SmoothTime);
            }
        }
    }
}
