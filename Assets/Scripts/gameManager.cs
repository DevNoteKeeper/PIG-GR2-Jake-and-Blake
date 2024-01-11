using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class gameManager : MonoBehaviour
{
    
    public Text timeTxt;
    [SerializeField] 
    public float totalTime = 60f;
    private float currentTime;

    public bool isTimeOVer = false;

    // Start is called before the first frame update
    void Start()
    {
        currentTime = totalTime;
        isTimeOVer = false;
    }



    // Update is called once per frame
    void Update()
    {
        TimeFlow();
    }

    void TimeFlow(){
        if(currentTime <= 0){
            isTimeOVer = true;
            currentTime = 0f;
        } else{
            currentTime -= Time.deltaTime;
            UpdateTimeDisplay();
        }
    }

    void UpdateTimeDisplay(){
        int minutes = Mathf.FloorToInt(currentTime / 60);
        int seconds = Mathf.FloorToInt(currentTime % 60);

        timeTxt.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }


    public void Home(){
        SceneManager.LoadScene("StartScene");
    }

    public void LevelSelect(){
        SceneManager.LoadScene("LevelSelect");
    }
}

