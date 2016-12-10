using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class SlowBuff : MonoBehaviour, Buff {

    private float old;
    private float countDown = 4;

    //void Update()
    //{
    //    countDown -= Time.deltaTime;
    //    if (countDown < 0)
    //    {

    //    }
    //}

    public void DoAction(EnemyController enemyController)
    {
        Debug.Log("Slow Enemy!");
        old = enemyController.GetComponent<AIPath>().speed;
        enemyController.GetComponent<AIPath>().speed = old * 0.1f;
    }
}
