using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun
{
    public readonly string Name;

    public int Ammo;
    public readonly int MaxAmmo;

    public readonly float BaseDamage;

    public Bullet GetBullet()
    {
        return new Bullet(this, BaseDamage);
    }

    public Gun(string name, int maxAmmo, float baseDamage)
    {
        Name = name;
        MaxAmmo = maxAmmo;
        BaseDamage = baseDamage;
    }
}
