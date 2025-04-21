using Assets.Enums;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene((int)Scenes.LevelOne);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
