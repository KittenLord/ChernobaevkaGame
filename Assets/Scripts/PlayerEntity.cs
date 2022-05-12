using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEntity : Entity
{
    void Start()
    {
        Health = 1000;
        OnDamage += (Bullet b) =>
        {
            //Debug.Log(Health);
        };
    }


    void Update()
    {
        
    }
}
