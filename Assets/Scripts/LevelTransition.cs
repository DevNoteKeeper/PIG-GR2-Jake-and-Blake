using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Introduction_scene : MonoBehaviour
{
    public GameObject storyCanvas;
    public float storyDuration = 15f;

    void Start()
    {
        ShowStoryCanvas();
    }

    void ShowStoryCanvas()
    {
        storyCanvas.SetActive(true);
        Invoke("HideStoryCanvas", storyDuration);
    }

    void HideStoryCanvas()
    {
        storyCanvas.SetActive(false);
    }

}
