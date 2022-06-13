using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TankEnemy : BaseEnemy
{
    public Gun Gun;
    public Transform ShootPoint;
    public Transform BulletHolder;
    private int LastShot;

    public Transform Body;
    public Transform Turret;

    public bool DeactivateTurretRotation = false;

    private List<GameObject> civilians = new List<GameObject>();
    void Start()
    {
        TriggerEnterAction += CivilianTriggerEnter;
        TriggerExitAction += CivilianTriggerExit;

        Gun = new Gun("Gun", 1,         // Ammo
                          false,        // Automatic
                AmmoType.Rocket,        // Ammo Type
                             20,         // Damage
                             100,        // Shooting Speed
                            300,         // Recoil
                             1,         // Max Ammo
                            10,         // Accuracy
                             1,        // Shot Count
                             0,          // Reload Time (in seconds)
                             20         // Bullet Speed
        );
    }

    private void FixedUpdate()
    {
        DeactivateMovement = civilians.Any(c => (c.transform.position - transform.position).magnitude > 1.5);

        Turret.position = new Vector3(Body.position.x, Turret.position.y, Body.position.z);

        LastShot++;

        if (mainTarget == null)
        {
            mainTarget = baseTarget.gameObject;
        }

        if (!DeactivateTurretRotation) mu.RotateTowards(mainTarget.transform.position, Turret, TurningSpeed);

        var angle = Vector3.Angle(Turret.forward, mainTargetPosition - Turret.position);

        if (angle <= 5)
        {
            if (LastShot >= Gun.BaseShootingSpeed && (mainTargetPosition - transform.position).magnitude < NoticeRadius && !DeactivateShooting)
            {
                LastShot = 0;
                bg.Shoot(Gun, ShootPoint, recoilRB, new List<string> { "Tank", "Soldier" });
            }
        }





        if ((mainTarget.transform.position - transform.position).magnitude > ClosestDistance)
        {
            if(!DeactivateRotation) mu.RotateTowards(mainTarget.transform.position, transform, TurningSpeed);

            var a = Vector3.Angle(transform.forward, mainTargetPosition - transform.position);

            if(a > 90)
            {
                transform.rotation *= Quaternion.Euler(0, 180, 0);
                Turret.rotation *= Quaternion.Euler(0, 180, 0);
            }

            if (a <= 60 && !DeactivateMovement)
            {
                mu.MoveInDirection(transform, transform.forward, Speed, ref velocity, SmoothTime);
            }
        }
    }

    private void CivilianTriggerEnter(Collider c)
    {
        if (c.tag == "Civilian")
        {
            if (!civilians.Contains(c.gameObject))
            {
                civilians.Add(c.gameObject);
            }
        }
    }

    private void CivilianTriggerExit(Collider c)
    {
        if (c.tag == "Civilian")
        {
            if (civilians.Contains(c.gameObject))
            {
                civilians.Remove(c.gameObject);
            }
        }
    }
}
