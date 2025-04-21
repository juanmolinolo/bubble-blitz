using Assets.Enums;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject winMenu;
    public GameObject loseMenu;
    public Scene nextLevel;

    private readonly List<GameObject> bubbles = new();

    public void RemoveBubble(GameObject bubble)
    {
        bubbles.Remove(bubble);
        if (bubbles.Count == 0)
        {
            ShowWinMenu();
        }
    }

    public void AddBubble(GameObject bubble)
    {
        bubbles.Add(bubble);
    }

    #region Menu buttons

    #region Showers

    public void ShowPauseMenu()
    {
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
    }

    public void HidePauseMenu()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
    }

    public void ShowWinMenu()
    {
        Time.timeScale = 0;
        winMenu.SetActive(true);
    }

    public void ShowLoseMenu()
    {
        Time.timeScale = 0;
        loseMenu.SetActive(true);
    }

    #endregion Showers

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
        SceneManager.LoadScene(nextLevel.buildIndex);
    }

    #endregion Menu buttons
}
