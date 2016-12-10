using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    public PlayingFieldController playingField;
    [SerializeField] private Text scoreText;
    [SerializeField] private Text scoreMaxText;
    [SerializeField] private Slider scoreMaxSlider;

    private float score = 0.0f;
    private float scoreMax = 0.0f;
    private float maxDistance = 0.0f;

    public EnemyController enemy;
    void Update()
    {
        if (IsGameRunning())
        {
            score += Time.deltaTime;
        }

        updateIngameUI();
    }

    void Start()
    {
        initIngameUI();
    }

    private void updateIngameUI()
    {
        scoreText.text = string.Format("{0:0.##}", score);
        scoreMaxSlider.value =
           1 - (enemy.transform.position - enemy.playingFieldController.getEndField().transform.position).magnitude /
            maxDistance;
    }

    private void updateHighscore()
    {
        scoreMax = PlayerPrefs.GetFloat("Highscore");
        scoreMaxText.text = string.Format("{0:0.##}", scoreMax);
    }

    private void initIngameUI()
    {
        score = 0.0f;
        maxDistance = (enemy.transform.position - enemy.playingFieldController.getEndField().transform.position).magnitude;
        updateHighscore();
        updateIngameUI();
    }

    public void StartGame()
    {
        FindObjectOfType<AstarPath>().Scan();

        enemy.transform.position = playingField.getStartPosition() + Vector3.up * 0.1f;
        enemy.transform.rotation = Quaternion.identity;
        enemy.gameObject.SetActive(true);

        initIngameUI();

        foreach (CornerHighlightController corner in FindObjectsOfType<CornerHighlightController>())
        {
            foreach (Collider collider in corner.GetComponentsInChildren<Collider>())
            {
                collider.enabled = false;
            }
            foreach (Renderer renderer in corner.GetComponentsInChildren<Renderer>())
            {
                renderer.enabled = false;
            }
        }
    }

    public void StopGame()
    {
        if (score > scoreMax)
        {
            PlayerPrefs.SetFloat("Highscore", score);
            updateHighscore();
        }

        enemy.gameObject.SetActive(false);
        foreach (CornerHighlightController corner in FindObjectsOfType<CornerHighlightController>())
        {
            foreach (Collider collider in corner.GetComponentsInChildren<Collider>())
            {
                collider.enabled = true;
            }
            foreach (Renderer renderer in corner.GetComponentsInChildren<Renderer>())
            {
                renderer.enabled = true;
            }
        }
    }

    public bool IsGameRunning()
    {
        return enemy.gameObject.activeInHierarchy;
    }
}
