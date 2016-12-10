using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    public PlayingFieldController playingField;
    [SerializeField]
    private Text scoreText;
    private float score = 0.0f;
    public EnemyController enemy;

    public AudioSource buildMusic;
    public AudioSource runMusic;
    void Update()
    {
        if (IsGameRunning())
        {
            score += Time.deltaTime;
        }

        scoreText.text = string.Format("{0:0.##}", score);
    }

    public void StartGame()
    {
        buildMusic.Stop();
        runMusic.Play();
        FindObjectOfType<AstarPath>().Scan();

        score = 0.0f;
        enemy.transform.position = playingField.getStartPosition() + Vector3.up * 0.1f;
        enemy.transform.rotation = Quaternion.identity;
        enemy.gameObject.SetActive(true);

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
        runMusic.Stop();
        buildMusic.Play();
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
