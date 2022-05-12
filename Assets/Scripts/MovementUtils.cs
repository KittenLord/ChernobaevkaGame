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
        var direction = Globals.AngleDir(transform.forward, (target.transform.position - transform.position).normalized);

        direction /= Mathf.Abs(direction);

        if (direction == 0) direction = 1;

        var angle = Vector3.Angle(transform.forward, target.transform.position - transform.position);

        if (angle < TurningSpeed || angle <= 5)
            transform.LookAt(target.transform.position);
        else
            transform.Rotate(new Vector3(0, direction * TurningSpeed, 0));
    }

    public void MoveInDirection(Transform transform, Vector3 direction, float Speed, ref Vector3 velocity, float SmoothTime)
    {
        direction = direction.normalized;

        var nextPosition = Vector3.SmoothDamp(transform.position, transform.position + direction * Speed, ref velocity, SmoothTime);

        transform.position = nextPosition;
    }
}
