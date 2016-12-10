using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class SlowBuff : MonoBehaviour, Buff
{
    private float old;
    public float slowFactor = 0.05f;

    void Update()
    {
    }

    public void DoAction(EnemyController enemyController)
    {
        Debug.Log("Slow Enemy!");
        old = enemyController.GetComponent<AIPath>().speed;
        enemyController.GetComponent<AIPath>().speed = old * slowFactor;
    }

    public void UnDoAction(EnemyController enemyController)
    {
        Debug.Log("Un-Slow Enemy!");
        enemyController.GetComponent<AIPath>().speed = old;
    }
}
