using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEntity : Entity
{
    protected virtual void Start()
    {
        OnDamage += (Projectile p) =>
        {
            Debug.Log($"{gameObject.name}: {Health}");
        };

        OnDeath += () =>
        {
            Debug.Log($"{gameObject.name} ded");
            Destroy(gameObject);
        };
    }

    void Update()
    {
        
    }
}
