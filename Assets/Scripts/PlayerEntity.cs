using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEntity : Entity
{
    void Start()
    {
        Health = 1000;
        OnDamage += (Projectile p) =>
        {
            Debug.Log(Health);
        };
    }


    void Update()
    {
        
    }
}
