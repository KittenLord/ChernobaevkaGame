using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletGenerator : MonoBehaviour
{
    public Bullet BulletPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void Shoot(Gun gun, Transform shootPoint, Transform bulletHolder, Rigidbody rb, List<string> ignoreList)
    {
        for (int i = 0; i < gun.ShotCount; i++)
            GenerateBullet(gun, shootPoint, bulletHolder, ignoreList);

        rb.AddForce(-transform.forward * gun.Recoil);

        gun.Ammo--;
    }

    public void GenerateBullet(Gun gun, Transform shootPoint, Transform bulletHolder, List<string> ignoreList)
    {
        var bullet = Instantiate<Bullet>(BulletPrefab, shootPoint.position, Quaternion.identity, bulletHolder);

        var direction = shootPoint.transform.forward;

        float min = -gun.Accuracy / 2.0f;
        float max = gun.Accuracy / 2.0f;

        direction = Quaternion.Euler(new Vector3(Random.Range(min, max) / 10.0f, Random.Range(min, max), 0)) * direction;

        bullet.SetBullet(gun, direction, ignoreList, new List<string>());
    }
}
