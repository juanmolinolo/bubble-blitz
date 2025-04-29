using Assets.Enums;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    private GameObject mainMenu;

    [SerializeField]
    private GameObject levelPickerMenu;

    [SerializeField]
    private GameObject guideMenu;

    public void Play()
    {
        mainMenu.SetActive(false);
        levelPickerMenu.SetActive(true);
    }

    public void Guide()
    {
        mainMenu.SetActive(false);
        guideMenu.SetActive(true);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Back()
    {
        levelPickerMenu.SetActive(false);
        guideMenu.SetActive(false);
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

    public void LoadLevelThree()
    {
        SceneManager.LoadScene((int)Scenes.LevelThree);
    }
}
