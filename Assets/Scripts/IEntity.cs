using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEntity
{
    public float Health { get; set; }
    public float Armor { get; set; }

    public event Globals.EntityDie OnDeath;
    public event Globals.EntityDamage OnDamage;

    public void Damage(Bullet b);
}

public abstract class Entity : MonoBehaviour, IEntity
{
    public float Health { get; set; }
    public float Armor { get; set; }

    public event Globals.EntityDie OnDeath;
    public event Globals.EntityDamage OnDamage;

    public void ReduceHealth(Bullet bullet)
    {
        var damage = bullet.Gun.Damage;

        damage -= (Armor / 100.0f) * damage;

        Health -= damage;

        if(Health <= 0)
        {
            OnDeath.Invoke();
        }
    }

    public void Awake()
    {
        OnDamage += ReduceHealth;
    }

    public void Damage(Bullet b)
    {
        OnDamage.Invoke(b);
    }

    //protected virtual void OnCollisionEnter(Collision other)
    //{
    //    if(other.transform.tag == "Bullet")
    //    {
    //        OnDamage.Invoke(other.gameObject.GetComponent<Bullet>());
    //        Destroy(other.gameObject);
    //    }
    //}
}