using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    public PlayingFieldController playingField;
    [SerializeField]
    private Text scoreText;
    [SerializeField]
    private Text scoreMaxText;
    [SerializeField]
    private Slider scoreMaxSlider;

    private float score = 0.0f;
    private float scoreMax = 0.0f;
    private float maxDistance = 0.0f;

    public float musicFadeTime = 3.0f;

    public EnemyController enemy;

    public AudioSource buildMusic;
    public AudioSource runMusic;
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

        StartCoroutine("FadeIn", buildMusic);
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
        StopCoroutine("FadeOut");
        StartCoroutine("FadeOut", buildMusic);
        StopCoroutine("FadeIn");
        StartCoroutine("FadeIn", runMusic);
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
        StopCoroutine("FadeOut");
        StartCoroutine("FadeOut", runMusic);
        StopCoroutine("FadeIn");
        StartCoroutine("FadeIn", buildMusic);
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

    public IEnumerator FadeOut(AudioSource audioSource)
    {
        float startVolume = audioSource.volume;

        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / this.musicFadeTime;
            yield return null;
        }
        audioSource.Stop();
        audioSource.volume = startVolume;
    }

    public IEnumerator FadeIn(AudioSource audioSource)
    {
        float endVolume = audioSource.volume;
        audioSource.volume = 0.0f;
        audioSource.Play();

        while (audioSource.volume < endVolume)
        {
            audioSource.volume += endVolume * Time.deltaTime / this.musicFadeTime;
            yield return null;
        }
        audioSource.volume = endVolume;
    }

}
