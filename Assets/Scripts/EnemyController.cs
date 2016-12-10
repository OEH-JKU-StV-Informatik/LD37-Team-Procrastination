using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    public GameObject playingField;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        var playingFieldController = playingField.GetComponent<PlayingFieldController>();

        //if (playingFieldController.canGoXPlus())
        //{
        //    // goX+
        //}
        //...
    }


}
