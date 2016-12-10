using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    public PlayingFieldController playingFieldController;
    private AIPath aiPath;
    private StartButton startButton;
    // Use this for initialization
    void Start()
    {
        aiPath = GetComponent<AIPath>();
        startButton = FindObjectOfType<StartButton>();
    }

    // Update is called once per frame
    void Update()
    {
        if (aiPath.target == null && playingFieldController.getEndField() != null)
        {
            aiPath.target = playingFieldController.getEndField().transform;
        }

        if (aiPath.TargetReached && gameObject.activeInHierarchy)
        {
            startButton.StartClicked();
        }
    }


}
