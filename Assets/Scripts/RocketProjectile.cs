using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketProjectile : Projectile
{
    private Rigidbody rb;
    private bool started;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        if(!started && Speed > 0)
        {
            started = true;

            rb.AddForce(Direction * Speed, ForceMode.Impulse);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        var nearby = Physics.OverlapSphere(transform.position, 8);
        List<GameObject> entities = new List<GameObject>();

        foreach (var n in nearby)
        {
            if (n.GetComponent<IEntity>() != null)
            {
                entities.Add(n.gameObject);
            }
        }

        foreach (var e in entities)
        {
            if (!IgnoreList.Contains(e.tag))
            {
                e.GetComponent<IEntity>().Damage(this);
            }
        }

        Destroy(gameObject);

        //if (Globals.ImpenetrableObjectsTags.Contains(collision.transform.tag))
        //{
        //}
    }
}
