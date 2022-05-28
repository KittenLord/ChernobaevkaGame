using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Globals
{
    public delegate void EntityDie();

    public delegate void EntityDamage(Bullet bullet);
    public static int GetTargetPriority(string tag)
    {
        if (!EnemyTargetTags.Contains(tag)) return 0;
        switch (tag)
        {
            case "Base":
                return 100;
            case "Player":
                return 100;
            default:
                return 0;
        }
    }

    public static List<string> EnemyTargetTags = new List<string> { "Base", "Player"};

    public static List<string> ImpenetrableObjectsTags = new List<string> { "Base" };

    public static float AngleDir(Vector3 A, Vector3 B)
    {
        return A.x * -B.z + A.z * B.x;
    }

    public static Vector3 GetAverageGroupPosition(List<GameObject> members)
    {
        List<GameObject> groupMembers = new List<GameObject>(members);
        //groupMembers.Add(thisObject);

        Vector3 average = Vector3.zero;
        foreach (var v in groupMembers)
        {
            average += v.transform.position;
        }
        average /= (float)groupMembers.Count;

        return average;
    }

    

}
