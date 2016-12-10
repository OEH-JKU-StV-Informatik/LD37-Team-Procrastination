using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffTriggerController : MonoBehaviour {

    public Buff buff;

    // Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider other)
    {
        EnemyController enemy = other.gameObject.GetComponentInParent<EnemyController>();
        if (enemy)
        {
            GetComponent<Collider>().enabled = false;
            buff.DoAction(enemy);
        }
    }
}
