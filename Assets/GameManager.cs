using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Player player;
    public Text timeTxt;
    public float timeLimit = 60f;
    public GameObject SuccessModal;
    public GameObject FailureModal;

    private float timer;
    private bool hasReachedDoor = false;
    private bool isTimeOver = false;
    
    public float restartDelay=2f;
    private bool gameHasEnded = false;


    void Start(){
        //from first scene begin
        // SceneManager.LoadScene("StartScene");
        timer = timeLimit;
        SuccessModal.SetActive(false); 
        FailureModal.SetActive(false); 

    }

    void Update(){
       if(timer <= 0){
            isTimeOver = true;
            timer = 0f;
        } else{
            timer-= Time.deltaTime;
            UpdateTimeDisplay();
        }   
        ShowModal();
    }

    void ShowModal(){
        if(isTimeOver==false && hasReachedDoor){
            SuccessModal.SetActive(true); 
        }
        if(player.transform.position.y < -5f ||isTimeOver==true)
        {
            timer = 0f;
            FailureModal.SetActive(true); 
            player.enabled=false;
        }
    }


    void UpdateTimeDisplay(){
        int minutes = Mathf.FloorToInt(timer / 60);
        int seconds = Mathf.FloorToInt(timer % 60);

        timeTxt.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

// succeeding condition one
   
   private void OnTriggerEnter2D(Collider2D collision) {
       if(collision.tag == "door") {
           hasReachedDoor=true;
       }
    }
// failing condition one (obstacle)
    private void OnCollisionEnter2D(Collision2D other) {
        if(other.collider.tag == "" ){
            player.enabled = false;
            gameHasEnded = true;
        }
    }
}