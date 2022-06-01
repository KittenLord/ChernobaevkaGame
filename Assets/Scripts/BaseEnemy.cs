using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : MonoBehaviour
{
    public float Speed;
    public float TurningSpeed;
    public float SmoothTime;

    public float NoticeRadius;
    public float ClosestDistance;
    public float MaxViewDistance;

    protected Rigidbody recoilRB;
    protected BulletGenerator bg;
    protected MovementUtils mu;


    public SphereCollider FieldOfView;


    public bool DeactivateMovement = false;
    public bool DeactivateRotation = false;
    public bool DeactivateShooting = false;


    protected List<GameObject> targets;
    protected List<GameObject> following;
    protected GameObject mainTarget;
    public GameObject MainTarget => mainTarget;
    protected GameObject baseTarget;
    protected Vector3 mainTargetPosition => new Vector3(mainTarget.transform.position.x, transform.position.y, mainTarget.transform.position.z);
    protected Vector3 velocity;

    public event Action OnAwake;
    public event Action<Collider> TriggerEnterAction;
    public event Action<Collider> TriggerExitAction;

    private void Awake()
    {
        FieldOfView.radius = NoticeRadius;
        targets = new List<GameObject>();
        following = new List<GameObject>();
        recoilRB = GetComponent<Rigidbody>();
        bg = GameObject.FindGameObjectWithTag("GameManager").GetComponent<BulletGenerator>();
        mu = GameObject.FindGameObjectWithTag("GameManager").GetComponent<MovementUtils>();
        baseTarget = GameObject.FindGameObjectWithTag("Base");

        OnAwake?.Invoke();
    }



    protected void SetTarget()
    {
        if (targets.Count == 0)
        {
            mainTarget = null;
            return;
        }

        int probabilityPool = 0;
        foreach (var t in targets)
        {
            probabilityPool += Globals.GetTargetPriority(t.tag);
        }

        int result = UnityEngine.Random.Range(0, probabilityPool);

        int current = 0;
        foreach (var t in targets)
        {
            var probability = Globals.GetTargetPriority(t.tag);

            if (result >= current && result < current + probability)
            {
                mainTarget = t;
                return;
            }

            current = probability;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (Globals.EnemyTargetTags.Contains(other.tag))
        {
            if (!targets.Contains(other.gameObject))
            {
                targets.Add(other.gameObject);
                SetTarget();
            }
        }

        TriggerEnterAction?.Invoke(other);

        if (other.tag == "Soldier")
        {
            if (!following.Contains(other.gameObject))
            {
                following.Add(other.gameObject);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (Globals.EnemyTargetTags.Contains(other.tag))
        {
            if (targets.Contains(other.gameObject))
            {
                StartCoroutine(RemoveTargetCoroutine(other.gameObject));
            }
        }

        TriggerExitAction?.Invoke(other);

        if (other.tag == "Soldier")
        {
            if (following.Contains(other.gameObject))
            {
                following.Remove(other.gameObject);
            }
        }
    }
    protected IEnumerator RemoveTargetCoroutine(GameObject g)
    {
        int counter = 0;

        while (counter < 200 && counter >= 0)
        {
            counter++;
            if ((g.transform.position - transform.position).magnitude > MaxViewDistance)
            {
                targets.Remove(g);
                SetTarget();
                counter = -1;
            }
            yield return null;
        }

        if ((g.transform.position - transform.position).magnitude > NoticeRadius && counter > 0)
        {
            targets.Remove(g);
            SetTarget();
        }
    }
}
