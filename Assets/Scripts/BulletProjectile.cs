using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletProjectile : Projectile
{
    //public override AmmoType Type { get; set; } = AmmoType.Bullet;

    private void Start()
    {

    }

    private void Update()
    {
        transform.Translate(Direction * Speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        var target = collision.gameObject.GetComponent<IEntity>();
        if(target != null && !IgnoreList.Contains(collision.transform.tag))
        {
            target.Damage(this);
        }

        if (Globals.ImpenetrableObjectsTags.Contains(collision.transform.tag))
        {
            Destroy(gameObject);
        }
    }
}
