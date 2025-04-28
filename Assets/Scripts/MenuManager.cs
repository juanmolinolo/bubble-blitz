using Assets.Enums;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    private GameObject mainMenu;

    [SerializeField]
    private GameObject levelPickerMenu;

    public void Play()
    {
        mainMenu.SetActive(false);
        levelPickerMenu.SetActive(true);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Back()
    {
        levelPickerMenu.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void LoadLevelOne()
    {
        SceneManager.LoadScene((int)Scenes.LevelOne);
    }

    public void LoadLevelTwo()
    {
        SceneManager.LoadScene((int)Scenes.LevelTwo);
    }
}
