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
    private List<string> IgnoreList;
    private List<string> ImpenetrateList;

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

    public void SetBullet(Gun gun, Vector3 dir, List<string> ignoreList, List<string> impenetrateList)
    {
        ImpenetrateList = new List<string>(Globals.ImpenetrableObjectsTags);
        Gun = gun;
        Direction = dir;
        IgnoreList = ignoreList;
        if (impenetrateList != null) ImpenetrateList.AddRange(impenetrateList);
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
