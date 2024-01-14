using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Player player;
    public GameObject SuccessModal;
    public GameObject FailureModal;
    public GameObject JackPrefab;
    public GameObject BlackPrefab;
    public GameObject StringPrefab;

    private GameObject Jack;
    private GameObject Black;  // 수정: BlackPrefab을 Black으로 변경

    private bool hasReachedDoor = false;
    
    public float restartDelay = 2f;
    private bool gameHasEnded = false;

    public Timer timer;

    void Start()
    {
        SuccessModal.SetActive(false);
        FailureModal.SetActive(false);

        Jack = Instantiate(JackPrefab, new Vector3(-7, 1, 0), Quaternion.identity);
        Black = Instantiate(BlackPrefab, new Vector3(-7, 1, 0), Quaternion.identity);

        Instantiate(StringPrefab, new Vector3(-6, -2, 0), Quaternion.identity);
    }

    void Update()
    {
        ShowModal();
    }

    void ShowModal()
{
    if (Jack != null && Black != null)  
    {
        if (timer.isTimeOver == false && hasReachedDoor)
        {
            SuccessModal.SetActive(true);
        }

        if (Jack.transform.position.y < -5f || Black.transform.position.y < -5f || timer.isTimeOver == true)
        {
            FailureModal.SetActive(true);
            player.enabled = false;
        }
    }
}


    // succeeding condition one
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "door")
        {
            hasReachedDoor = true;
        }
    }

    // failing condition one (obstacle)
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.tag == "mushroom")
        {
            player.enabled = false;
            gameHasEnded = true;
        }
    }
}
