
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    public Rigidbody backgroundLight;
    public LevelSelection levelSelector;
    public PlayingFieldController playingField;
    public EnemyController enemyPrototype;
    public StartButton startButton;
    [SerializeField]
    private TextMesh wallText;
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


    private EnemyController enemy;

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
        wallText.text = "x" + (PlayingFieldController.maxWalls - PlayingFieldController.currentWalls);

        updateIngameUI();
    }

    void Start()
    {
        backgroundLight.AddForce(Random.onUnitSphere*100);
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
        enemy = Instantiate(enemyPrototype, playingField.GetStartPosition(), Quaternion.identity);
        enemy.SetTarget(playingField.GetEndField().transform);
        enemy.SetGame(this);

        ResetVolumes();
        StopCoroutine("FadeOut");
        StartCoroutine("FadeOut", buildMusic);
        StopCoroutine("FadeIn");
        StartCoroutine("FadeIn", runMusic);
        FindObjectOfType<AstarPath>().Scan();
        playingField.IsValidLevel();

        maxDistance = (playingField.GetStartPosition() - playingField.GetEndField().transform.position).magnitude;

        enemy.transform.position = playingField.GetStartPosition() + Vector3.up * 0.1f;
        enemy.transform.rotation = Quaternion.identity;

        initIngameUI();

        foreach (CornerHighlightController corner in FindObjectsOfType<CornerHighlightController>())
        {
            Destroy(corner.gameObject);
        }
    }

    public void StopGame()
    {
        DestroyImmediate(enemy.gameObject);
        startButton.updateButton();

        ResetVolumes();
        StopCoroutine("FadeOut");
        StartCoroutine("FadeOut", runMusic);
        StopCoroutine("FadeIn");
        StartCoroutine("FadeIn", buildMusic);
        levelSelector.levelGenerator.generateCorners();

        //update UI and Highscore
        if (score > scoreMax)
        {
            PlayerPrefs.SetFloat("Highscore_" + FindObjectOfType<LevelGenerator>().selectedLevel, score);
            updateHighscore();
            if (score > FindObjectOfType<LevelGenerator>().selectedLevelTimeWin)
            {
                Resources.FindObjectsOfTypeAll<ErrorText>()[0].DisplaySuccess("Win!" + System.Environment.NewLine +
                                                                              "<size=18>You won the level and set a new highscore</size>", 4);
                levelSelector.NextLevel();
            }
            else
            {
                Resources.FindObjectsOfTypeAll<ErrorText>()[0].DisplaySuccess("New Highscore!" + System.Environment.NewLine +
                                                                              "<size=18>The level is not yet complete though</size>", 4);
            }
        }
    }

    public bool IsGameRunning()
    {
        return enemy != null;
    }

    private void updateIngameUI()
    {
        scoreText.text = string.Format("{0:0.##}", score);
        scoreMaxSlider.value = IsGameRunning() ?
           1 - ((enemy.transform.position - playingField.GetEndField().transform.position).magnitude /
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
