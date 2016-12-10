using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Buff
{
    void DoAction(EnemyController enemyController);
    void UnDoAction(EnemyController enemyController);
}
