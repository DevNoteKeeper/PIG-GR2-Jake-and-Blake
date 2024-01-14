using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text timeTxt;
    public float totalTime = 60f;
    private float currentTime;

    public bool isTimeOver = false;

    public void Start()
    {
        currentTime = totalTime;
        isTimeOver = false;
    }

    public void Update()
    {
        TimeFlow();
    }

    public void TimeFlow()
    {
        if (currentTime <= 0)
        {
            isTimeOver = true;
            currentTime = 0f;
        }
        else
        {
            currentTime -= Time.deltaTime;
            UpdateTimeDisplay();
        }
    }

    public void UpdateTimeDisplay()
    {
        int minutes = Mathf.FloorToInt(currentTime / 60);
        int seconds = Mathf.FloorToInt(currentTime % 60);

        timeTxt.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
