using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffController : MonoBehaviour
{

    public GameObject playingField;
    public GameObject buffTriggerPrototype;
    public GameObject slowBuffPrototype;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public GameObject placeBuffTrigger(Vector3 position, GameObject buff)
    {
        GameObject newBuffTrigger = Instantiate(buffTriggerPrototype, position, Quaternion.identity, playingField.transform);
        Instantiate(buff, position, Quaternion.identity, playingField.transform);
        newBuffTrigger.GetComponent<BuffTriggerController>().buff = buff;
        return newBuffTrigger;
    }
}
