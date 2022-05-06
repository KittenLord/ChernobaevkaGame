using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEntity
{
    public float Health { get; set; }
    public float Armor { get; set; }

    public event Globals.EntityDie OnDeath;
    public event Globals.EntityDamage OnDamage;
}

public abstract class Entity : MonoBehaviour, IEntity
{
    public float Health { get; set; }
    public float Armor { get; set; }

    public event Globals.EntityDie OnDeath;
    public event Globals.EntityDamage OnDamage;

    public void Damage(Bullet bullet)
    {
        var damage = bullet.Gun.Damage;

        damage -= (Armor / 100.0f) * damage;

        Health -= damage;

        if(Health <= 0)
        {
            OnDeath.Invoke();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Bullet")
        {
            OnDamage.Invoke(other.GetComponent<Bullet>());
        }
    }
}