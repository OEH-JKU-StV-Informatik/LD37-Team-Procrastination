using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffTriggerController : MonoBehaviour
{
    public GameObject buff;
    private float countDown = -1;
    private EnemyController enemy;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (countDown > 0)
        {
            countDown -= Time.deltaTime;
            if (countDown <= 0)
            {
                buff.GetComponent<Buff>().UnDoAction(enemy);
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
