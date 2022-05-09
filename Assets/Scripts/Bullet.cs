using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float Speed;


    public Gun Gun;
    private Vector3 Direction;

    private float CurrentLifetime;
    private float FinishLifetime;

    private void Start()
    {
        CurrentLifetime = 0;
        FinishLifetime = (Time.fixedDeltaTime * 50) * 25;
    }

    private void Update()
    {
        transform.Translate(Direction * Speed * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        CurrentLifetime += Time.fixedDeltaTime;
        if(CurrentLifetime >= FinishLifetime)
        {
            Destroy(gameObject);
        }
    }

    public void SetBullet(Gun gun, Vector3 dir)
    {
        Gun = gun;
        Direction = dir;
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    var target = collision.gameObject.GetComponent<IEntity>();
    //    if(target != null)
    //    {
    //        target.Damage(this);
    //    }
    //    Destroy(gameObject);
    //}
}
