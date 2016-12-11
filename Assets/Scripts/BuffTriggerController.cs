using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffTriggerController : MonoBehaviour
{
    public GameObject buff;
    private float countDown = -1;
    private EnemyController enemy;
    private Game game;
    // Use this for initialization
    void Start()
    {
        game = Resources.FindObjectsOfTypeAll<Game>()[0];
    }

    // Update is called once per frame
    void Update()
    {
        if (!game.IsGameRunning() && enemy != null)
        {
            countDown = -1;
            buff.GetComponent<Buff>().UnDoAction(enemy);
            GetComponent<Collider>().enabled = true;
            enemy = null;
            return;
        }

        if (countDown > 0)
        {
            countDown -= Time.deltaTime;
            if (countDown <= 0)
            {
                buff.GetComponent<Buff>().UnDoAction(enemy);
                GetComponent<Collider>().enabled = true;
                enemy = null;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        enemy = other.gameObject.GetComponentInParent<EnemyController>();
        if (enemy)
        {
            GetComponent<Collider>().enabled = false;
            buff.GetComponent<Buff>().DoAction(enemy);
            countDown = 4;
        }
    }
}
