using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public AmmoType Type;

    public float Speed;
    public Gun Gun;
    protected Vector3 Direction;

    protected float CurrentLifetime;
    protected float FinishLifetime;
    protected List<string> IgnoreList;
    protected List<string> ImpenetrateList;

    private void Awake()
    {
        CurrentLifetime = 0;
        FinishLifetime = (Time.fixedDeltaTime * 50) * 3;
    }

    private void FixedUpdate()
    {
        CurrentLifetime += Time.fixedDeltaTime;
        if (CurrentLifetime >= FinishLifetime)
        {
            Destroy(gameObject);
        }
    }

    public void SetProjectile(Gun gun, Vector3 dir, List<string> ignoreList, List<string> impenetrateList)
    {
        ImpenetrateList = new List<string>(Globals.ImpenetrableObjectsTags);
        Gun = gun;
        Speed = gun.BulletSpeed;
        Direction = dir;
        IgnoreList = ignoreList;
        if (impenetrateList != null) ImpenetrateList.AddRange(impenetrateList);
    }
}
