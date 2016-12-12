using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    
    private AIPath aiPath;
    private StartButton startButton;
    private Game game;

    // Use this for initialization
    void Start()
    {
        startButton = FindObjectOfType<StartButton>();
    }

    // Update is called once per frame
    void Update()
    {
        if (aiPath != null && aiPath.TargetReached && gameObject.activeInHierarchy)
        {
            game.StopGame();
        }
    }

    public void SetTarget(Transform t)
    {
        aiPath = gameObject.AddComponent<AIPath>();
        aiPath.speed = 10;
        aiPath.turningSpeed = 50;
        aiPath.slowdownDistance = 1;
        aiPath.pickNextWaypointDist = 2f;
        aiPath.forwardLook = 10;
        aiPath.endReachedDistance = 2;
        aiPath.target = t;
    }

    internal void SetGame(Game game)
    {
        this.game = game;
    }
}
