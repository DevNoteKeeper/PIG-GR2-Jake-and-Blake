using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SpringStart(){
        SceneManager.LoadScene("Spring");
    }

    public void SummerStart(){
        SceneManager.LoadScene("Summer");
    }

    public void AutumnStart(){
        SceneManager.LoadScene("Autumn");
    }

    public void WinterStart(){
        SceneManager.LoadScene("Winter");
    }

    

    // Update is called once per frame
    void Update()
    {
        
    }
}
