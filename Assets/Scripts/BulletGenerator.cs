using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletGenerator : MonoBehaviour
{
    public Transform ProjectileHolder;

    public BulletProjectile BulletPrefab;
    public RocketProjectile RocketPrefab;

    private List<Projectile> ProjectilesList = new List<Projectile>();

    // Start is called before the first frame update
    void Start()
    {
        ProjectilesList.Add(BulletPrefab);
        ProjectilesList.Add(RocketPrefab);
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void Shoot(Gun gun, Transform shootPoint, Rigidbody rb, List<string> ignoreList)
    {
        for (int i = 0; i < gun.ShotCount; i++)
            GenerateProjectile(gun, shootPoint, ignoreList);

        rb.AddForce(-transform.forward * gun.Recoil);

        gun.Ammo--;
    }

    public void GenerateProjectile(Gun gun, Transform shootPoint, List<string> ignoreList)
    {
        var projectilePrefab = ProjectilesList.Find(p => p.Type == gun.AmmoType);

        var projectileGO = Instantiate<Projectile>(projectilePrefab, shootPoint.position, Quaternion.identity, ProjectileHolder);

        var direction = shootPoint.transform.forward;

        float min = -gun.Accuracy / 2.0f;
        float max = gun.Accuracy / 2.0f;

        direction = Quaternion.Euler(new Vector3(0 /*Random.Range(min, max) / 10.0f*/, Random.Range(min, max), 0)) * direction;

        projectileGO.SetProjectile(gun, direction, ignoreList, new List<string>());
    }
}
