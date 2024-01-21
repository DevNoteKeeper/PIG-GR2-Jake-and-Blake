using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelect : MonoBehaviour
{
    public Button springButton;
    public Button summerButton;
    public Button autumnButton;
    public Button winterButton;

    public Image summerLockImage;
    public Image autumnLockImage;
    public Image winterLockImage;

    void Start()
    {
        // Load player data
        PlayerData loadedData = SaveSystem.LoadPlayerData();

        if (loadedData != null)
        {
            // Disable buttons for levels beyond the highest passed level
            int highestPassedLevel = loadedData.highestPassedLevel;

            // Set interactability for each button
            SetButtonInteractivity(springButton, true);
            SetButtonInteractivity(summerButton, highestPassedLevel >= 2);
            SetButtonInteractivity(autumnButton, highestPassedLevel >= 3);
            SetButtonInteractivity(winterButton, highestPassedLevel >= 4);

            // Hide lock images based on the highest passed level
            HideLockImage(summerLockImage, highestPassedLevel >= 2);
            HideLockImage(autumnLockImage, highestPassedLevel >= 3);
            HideLockImage(winterLockImage, highestPassedLevel >= 4);
        }
        else
        {
            // Handle the case when no saved data is found
        }
    }

    // Method to set button interactability
    void SetButtonInteractivity(Button button, bool interactable)
    {
        button.interactable = interactable;
    }

    // Method to hide or show lock images
    void HideLockImage(Image lockImage, bool hide)
    {
        lockImage.gameObject.SetActive(!hide);
    }

    public void SpringStart()
    {
        SceneManager.LoadScene("Spring");
    }

    public void SummerStart()
    {
        // Check if the player has passed Summer level
        PlayerData loadedData = SaveSystem.LoadPlayerData();
        if (loadedData != null && loadedData.highestPassedLevel >= 2)
        {
            SceneManager.LoadScene("Summer");
        }
        else
        {
            // Handle the case when the player hasn't passed the required level
            Debug.Log("Cannot access Summer level. Pass the previous levels first.");
        }
    }

    public void AutumnStart()
    {
        // Check if the player has passed Autumn level
        PlayerData loadedData = SaveSystem.LoadPlayerData();
        if (loadedData != null && loadedData.highestPassedLevel >= 3)
        {
            SceneManager.LoadScene("Autumn");
        }
        else
        {
            // Handle the case when the player hasn't passed the required level
            Debug.Log("Cannot access Autumn level. Pass the previous levels first.");
        }
    }

    public void WinterStart()
    {
        // Check if the player has passed Winter level
        PlayerData loadedData = SaveSystem.LoadPlayerData();
        if (loadedData != null && loadedData.highestPassedLevel >= 4)
        {
            SceneManager.LoadScene("Winter");
        }
        else
        {
            // Handle the case when the player hasn't passed the required level
            Debug.Log("Cannot access Winter level. Pass the previous levels first.");
        }
    }
}
