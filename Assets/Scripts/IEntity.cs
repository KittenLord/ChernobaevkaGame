using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEntity
{
    public float Health { get; set; }
    public float Armor { get; set; }

    public void Die();
    public void Damage(Bullet bullet);
}
