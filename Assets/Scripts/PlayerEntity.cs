using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEntity : Entity
{
    void Start()
    {
        OnDamage += (Projectile p) =>
        {
            Debug.Log($"{gameObject.name}: {Health}");
        };

        OnDeath += () =>
        {
            Debug.Log("Player ded");
        };
    }

    void Update()
    {
        
    }
}
