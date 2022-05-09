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
                return 70;
            default:
                break;
        }

        return 0;
    }

    public static List<string> EnemyTargetTags = new List<string> { "Base", "Player"};
}
