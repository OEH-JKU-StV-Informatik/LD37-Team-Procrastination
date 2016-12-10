using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    public PlayingFieldController playingFieldController;

    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<AIPath>().target == null && playingFieldController.getEndField() != null)
        {
            GetComponent<AIPath>().target =playingFieldController.getEndField().transform;
        }

        if (GetComponent<AIPath>().TargetReached)
        {
            FindObjectOfType<StartButton>().StartClicked();
        }
    }


}
