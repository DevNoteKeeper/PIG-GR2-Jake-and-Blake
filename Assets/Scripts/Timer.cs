using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text timeTxt;
    public float totalTime = 60f;
    private float currentTime;
    private bool isTimeOver = false;

    public bool IsTimeOver
    {
        get { return isTimeOver; }
        private set { isTimeOver = value; }
    }

    // Add this property to calculate completion time
    public float CompletionTime
    {
        get { return totalTime - currentTime; }
    }

    // Change Start method to Awake to ensure it is called before other Start methods
    private void Awake()
    {
        // Initialize the timer when the script starts
        InitializeTimer();
    }

    // Method to initialize the timer
    private void InitializeTimer()
    {
        // Set the current time to the total time, reset the time over flag, and update the time display
        currentTime = totalTime;
        isTimeOver = false;
        UpdateTimeDisplay(); // Display the time immediately after initialization
    }

    // Update is called once per frame
    private void Update()
    {
        // Update the time flow
        TimeFlow();
    }

    // Method to handle the flow of time
    private void TimeFlow()
    {
        // If time has run out, set the flag and set the timer to 0
        if (currentTime <= 0)
        {
            isTimeOver = true;
            currentTime = 0f;
        }
        else
        {
            // Decrease the current time based on real-time
            currentTime -= Time.deltaTime;
            UpdateTimeDisplay();
        }
    }

    // Method to update the time display in the UI
    private void UpdateTimeDisplay()
    {
        // Calculate minutes and seconds
        int minutes = Mathf.FloorToInt(currentTime / 60);
        int seconds = Mathf.FloorToInt(currentTime % 60);

        // Display the time as text
        timeTxt.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

}