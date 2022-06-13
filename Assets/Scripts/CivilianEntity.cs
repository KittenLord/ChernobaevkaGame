using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CivilianEntity : MonoBehaviour
{
    private List<GameObject> targets = new List<GameObject>();
    private GameObject mainTarget = null;
    private Vector3 mainTargetPosition => new Vector3(mainTarget.transform.position.x, transform.position.y, mainTarget.transform.position.z);
    private Vector3 idleDestination = Vector3.zero;
    private MovementUtils mu;

    public float Speed;
    public float IdleSpeed;
    public float TurningSpeed;
    public float NoticeRadius;
    public Transform FieldOfView;
    private Vector3 velocity;
    public float SmoothTime;

    void Start()
    {
        mu = GameObject.FindGameObjectWithTag("GameManager").GetComponent<MovementUtils>();

        FieldOfView.GetComponent<SphereCollider>().radius = NoticeRadius;
    }

    void Update()
    {

    }
    private void FixedUpdate()
    {
        if (mainTarget == null)
        {
            if (idleDestination == Vector3.zero)
            {
                idleDestination = transform.position + new Vector3(Random.Range(-2.0f, 2.0f), 0, Random.Range(-2.0f, 2.0f));

                if (Vector3.Angle(transform.forward, idleDestination - transform.position) > 90)
                {
                    idleDestination = transform.position - (idleDestination - transform.position);
                }
            }


            mu.RotateTowards(idleDestination, transform, TurningSpeed);

            var angle = Vector3.Angle(transform.forward, idleDestination - transform.position);

            if (angle <= 5)
            {
                mu.MoveInDirection(transform, idleDestination - transform.position, IdleSpeed, ref velocity, SmoothTime);

                if ((transform.position - idleDestination).magnitude <= 0.25f)
                {
                    idleDestination = Vector3.zero;
                }
            }
        }
        else
        {
            mu.RotateTowards(mainTargetPosition, transform, TurningSpeed);

            var angle = Vector3.Angle(transform.forward, mainTargetPosition - transform.position);

            if (angle <= 5)
            {
                mu.MoveInDirection(transform, mainTargetPosition - transform.position, Speed, ref velocity, SmoothTime);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Tank")
        {
            if (!targets.Contains(other.gameObject))
            {
                targets.Add(other.gameObject);
                if (Random.Range(0, 2) == 0 || targets.Count == 1)
                {
                    mainTarget = other.gameObject;
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Tank")
        {
            if (targets.Contains(other.gameObject))
            {
                targets.Remove(other.gameObject);

                mainTarget = targets.Count > 0 ? targets[Random.Range(0, targets.Count)] : null;
            }
        }
    }
}
