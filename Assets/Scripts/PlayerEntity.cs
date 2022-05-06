using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEntity : Entity
{


    void Start()
    {
        OnDamage += Damage;
        Health = 1000;
        OnDamage += (Bullet b) =>
        {
            Debug.Log(Health);
        };
    }


    void Update()
    {
        
    }
}
