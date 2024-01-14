using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonChangeScene1 : MonoBehaviour
{
    public Button[] buttons;
    public float restartDelay = 2f; 

    public void Start(){
        foreach (Button button in buttons)
        {
        if (button != null)
        {
            string buttonTag = button.tag;
            switch (buttonTag)
            {
                case "Home":
                    button.onClick.AddListener(goHome);
                break;
                
                case "Reply":
                    button.onClick.AddListener(startAgain);
                break;

                case "Next":
                   button.onClick.AddListener(ChangeScene);
                break;

            // 添加其他按钮的处理逻辑，如果有的话
                default:
                    Debug.LogWarning("Unhandled button tag: " + buttonTag);
                break;
            }
        }else
        {
            Debug.LogError("Button component not assigned to the script!");
        }
        }
    }

    public void ChangeScene(){
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }
    public void goHome(){
        SceneManager.LoadScene("StartScene");
    }
    public void startAgain(){
        Invoke("Restart",restartDelay);
    }

    public void Restart(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }

}
