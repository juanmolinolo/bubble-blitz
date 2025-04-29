using Assets.Enums;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Class properties

    [SerializeField]
    private GameObject levelMenu;

    [SerializeField]
    private GameObject pauseMenu;

    [SerializeField]
    private TextMeshProUGUI muteText;

    [SerializeField]
    private GameObject winMenu;

    [SerializeField]
    private TextMeshProUGUI scoreText;

    [SerializeField]
    private GameObject loseMenu;

    [SerializeField]
    private Scenes nextLevel;

    [SerializeField]
    internal int levelTime;

    internal int currentScore;

    internal int timeLeft;

    private readonly List<Bubble> activeBubbles = new();

    private double TimeLeftAsPercentage => (double)timeLeft / levelTime * 100;

    private bool IsMuted => AudioListener.volume == 0;

    private bool IsPauseDisabled => Time.timeScale == 0;

    #endregion Class properties

    #region Events

    private void Start()
    {
        timeLeft = levelTime;
        currentScore = 0;

        Time.timeScale = 0;
        levelMenu.SetActive(true);
        StartCoroutine(HideLevelMenu());
    }

    private IEnumerator HideLevelMenu()
    {
        yield return new WaitForSecondsRealtime(3f);
        levelMenu.SetActive(false);
        Time.timeScale = 1;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseMenu.activeSelf)
            {
                HidePauseMenu();
            }
            else
            {
                ShowPauseMenu();
            }
        }
    }

    #endregion Events

    public void RemoveBubble(Bubble bubble)
    {
        activeBubbles.Remove(bubble);
        currentScore += bubble.GetBubbleScore(TimeLeftAsPercentage);
        if (activeBubbles.Count == 0)
        {
            ShowWinMenu();
        }
    }

    public void AddBubble(Bubble bubble)
    {
        activeBubbles.Add(bubble);
    }

    public void MuteOrUnmute()
    {
        if (IsMuted)
        {
            AudioListener.volume = 1;
            muteText.text = "MUTE";
        }
        else
        {
            AudioListener.volume = 0;
            muteText.text = "UNMUTE";
        }
    }

    #region Menu interaction

    public void ShowPauseMenu()
    {
        if (!IsPauseDisabled)
        {
            Time.timeScale = 0;
            pauseMenu.SetActive(true);
        }
    }

    public void HidePauseMenu()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
    }

    public void ShowWinMenu()
    {
        scoreText.text = $"SCORE: {currentScore}";
        Time.timeScale = 0;
        winMenu.SetActive(true);
    }

    public void ShowLoseMenu()
    {
        Time.timeScale = 0;
        loseMenu.SetActive(true);
    }

    public void ReplayCurrentLevel()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoToMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene((int)Scenes.MainMenu);
    }

    public void GoToNextLevel()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene((int)nextLevel);
    }

    #endregion Menu interaction
}
