using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    public LevelSelection levelSelector;
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

    private bool displayMenu = false;

    public float musicFadeTime = 3.0f;

    public EnemyController enemy;

    public AudioSource buildMusic;
    private float buildVolume;
    public AudioSource runMusic;
    private float runVolume;
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
        buildVolume = buildMusic.volume;
        runVolume = runMusic.volume;
        initIngameUI();

        StartCoroutine("FadeIn", buildMusic);
    }

    void ResetVolumes()
    {
        buildMusic.volume = buildVolume;
        runMusic.volume = runVolume;
    }

    public void StartGame()
    {
        ResetVolumes();
        StopCoroutine("FadeOut");
        StartCoroutine("FadeOut", buildMusic);
        StopCoroutine("FadeIn");
        StartCoroutine("FadeIn", runMusic);
        FindObjectOfType<AstarPath>().Scan();

        maxDistance = (playingField.GetStartPosition() - playingField.GetEndField().transform.position).magnitude;

        enemy.transform.position = playingField.GetStartPosition() + Vector3.up * 0.1f;
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
            PlayerPrefs.SetFloat("Highscore_" + FindObjectOfType<LevelGenerator>().selectedLevel, score);
            if (score > FindObjectOfType<LevelGenerator>().selectedLevelTimeWin)
            {
                Resources.FindObjectsOfTypeAll<ErrorText>()[0].DisplaySuccess("Win!" + Environment.NewLine + 
                                                                              "You won the Level and set a new Highscore", 5);
                levelSelector.NextLevel();
            }
            updateHighscore();
        }
        ResetVolumes();
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

    private void updateIngameUI()
    {
        scoreText.text = string.Format("{0:0.##}", score);
        scoreMaxSlider.value = enemy.gameObject.activeInHierarchy ?
           1 - ((enemy.transform.position - enemy.playingFieldController.GetEndField().transform.position).magnitude /
                maxDistance) : scoreMaxSlider.value;
    }

    private void updateHighscore()
    {
        scoreMax = PlayerPrefs.GetFloat("Highscore_" + FindObjectOfType<LevelGenerator>().selectedLevel);
        scoreMaxText.text = string.Format("{0:0.##}", scoreMax);
    }

    public void initIngameUI()
    {
        score = 0.0f;
        updateHighscore();
        updateIngameUI();
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
