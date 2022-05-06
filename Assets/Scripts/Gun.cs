using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun
{
    public readonly string Name;

    public int Ammo;

    public readonly bool IsAutomatic;



    public readonly float BaseDamage; // TODO: Implement rocket/grenade launcher support
    public readonly float BaseShootingSpeed;
    public readonly float BaseRecoil;
    public readonly int   BaseMaxAmmo;
    public readonly float BaseAccuracy;
    public readonly int   BaseShotCount;
    public readonly float BaseReloadTime;



    public GunUpgrades Upgrades;
    public float Damage => BaseDamage + Upgrades.DamageUpgrade;                      // More => Better
    public float ShootingSpeed => BaseShootingSpeed + Upgrades.ShootingSpeedUpgrade; // Less => Better
    public float Recoil => BaseRecoil + Upgrades.RecoilUpgrade;                      // Less => Better
    public int MaxAmmo => BaseMaxAmmo + Upgrades.MaxAmmoUpgrade;                   // More => Better
    public float Accuracy => BaseAccuracy + Upgrades.AccuracyUpgrade;                // Less => Better
    public float ShotCount => BaseShotCount + Upgrades.ShotCountUpgrade;             // More => Better
    public float ReloadTime => BaseReloadTime + Upgrades.ReloadTimeUpgrade;

    public Gun(string name, int ammo, bool isAutomatic, float baseDamage, float baseShootingSpeed, float baseRecoil, int baseMaxAmmo, float baseAccuracy, int baseShotCount, float baseReloadTime)
    {
        Name = name;
        Ammo = ammo;
        IsAutomatic = isAutomatic;


        BaseDamage = baseDamage;
        BaseShootingSpeed = baseShootingSpeed;
        BaseRecoil = baseRecoil;
        BaseMaxAmmo = baseMaxAmmo;
        BaseAccuracy = baseAccuracy;
        BaseShotCount = baseShotCount;
        BaseReloadTime = baseReloadTime;


        Upgrades = new GunUpgrades(0, 0, 0, 0, 0, 0, 0);
    }
}

public struct GunUpgrades
{
    public float DamageUpgrade;
    public float ShootingSpeedUpgrade;
    public float RecoilUpgrade;
    public int   MaxAmmoUpgrade;
    public float AccuracyUpgrade;
    public int   ShotCountUpgrade;
    public float ReloadTimeUpgrade;

    public GunUpgrades(float damageUpgrade, float shootingSpeedUpgrade, float recoilUpgrade, int maxAmmoUpgrade, float accuracyUpgrade, int shotCount, float reloadTime)
    {
        DamageUpgrade = damageUpgrade;
        ShootingSpeedUpgrade = shootingSpeedUpgrade;
        RecoilUpgrade = recoilUpgrade;
        MaxAmmoUpgrade = maxAmmoUpgrade;
        AccuracyUpgrade = accuracyUpgrade;
        ShotCountUpgrade = shotCount;
        ReloadTimeUpgrade = reloadTime;
    }

}
