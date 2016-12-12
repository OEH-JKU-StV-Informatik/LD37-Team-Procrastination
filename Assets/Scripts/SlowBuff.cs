using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class SlowBuff : MonoBehaviour, Buff
{
    private float old;
    public float slowFactor = 0.05f;

    private static bool isSlowed = false;

    void Update()
    {
    }

    public void DoAction(EnemyController enemyController)
    {
        if (!isSlowed)
        {
            Debug.Log("Slow Enemy!");
            old = enemyController.getSpeed();
            enemyController.setSpeed(old * slowFactor);
            isSlowed = true;
        }
    }

    public void UnDoAction(EnemyController enemyController)
    {
        if (isSlowed)
        {
            Debug.Log("Un-Slow Enemy!");
            enemyController.setSpeed(old);
            isSlowed = false;
        }
    }
}
