using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet
{
    public Gun Gun;

    public float Damage;
    public Bullet(Gun gun, float damage)
    {
        Gun = gun;
        Damage = damage;
    }
}
