using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneLoader : MonoBehaviour
{
    public void OpenMainMenu()
    {
        SceneManager.LoadScene("StartScene");
    }

    public void StartGame()
    {
        SceneManager.LoadScene("LevelSelect");
    }

    public void OpenSettings()
    {
        SceneManager.LoadScene("Settings");
    }

    public void OpenHelp()
    {
        SceneManager.LoadScene("Help");
    }
}
