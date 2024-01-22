using UnityEngine;
using UnityEngine.SceneManagement;


public class StartScene : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("LevelSelect");
    }

    public void OpenSettings()
    {
        SceneManager.LoadScene("Settings");
    }
}
