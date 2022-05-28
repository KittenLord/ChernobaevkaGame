using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementUtils : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void RotateTowards(GameObject target, Transform transform, float TurningSpeed)
    {
        //var direction = Globals.AngleDir(transform.forward, (target.transform.position - transform.position).normalized);
        //
        //direction = direction >= 0 ? 1 : -1;
        //
        //var angle = Vector3.Angle(transform.forward, new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z) - transform.position);
        //
        //if (angle <= TurningSpeed || angle <= 5)
        //    transform.LookAt(target.transform.position);
        //else
        //    transform.Rotate(new Vector3(0, direction * TurningSpeed, 0));

        transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.forward, new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z) - transform.position, TurningSpeed * Mathf.Deg2Rad, 0.0f));
    }

    public void MoveInDirection(Transform transform, Vector3 direction, float Speed, ref Vector3 velocity, float SmoothTime)
    {
        direction = direction.normalized;
                var nextPosition = Vector3.SmoothDamp(transform.position, transform.position + direction * Speed, ref velocity, SmoothTime);

        transform.position = nextPosition;
    }
}
