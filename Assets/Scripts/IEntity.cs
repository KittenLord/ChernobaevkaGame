using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEntity
{
    public float Health { get; set; }
    public float Armor { get; set; }

    public event Globals.EntityDie OnDeath;
    public event Globals.EntityDamage OnDamage;

    public void Damage(Projectile projectile);
}

public abstract class Entity : MonoBehaviour, IEntity
{
    public float Health { get; set; }
    public float EntityHealth;
    public float Armor { get; set; }
    public float EntityArmor;

    public event Globals.EntityDie OnDeath;
    public event Globals.EntityDamage OnDamage;

    public void ReduceHealth(Projectile projectile)
    {
        var damage = projectile.Gun.Damage;

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
        Health = EntityHealth;
        Armor = EntityArmor;
    }

    public void Damage(Projectile projectile)
    {
        OnDamage.Invoke(projectile);
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