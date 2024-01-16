using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Player player;
    public GameObject SuccessModal;
    public GameObject FailureModal;
    public GameObject JackPrefab;
    public GameObject BlakePrefab;
    public GameObject StringPrefab;

    private GameObject Jack;
    private GameObject Blake; 

    private bool hasReachedDoor = false;
    
    public float restartDelay = 2f;
    private bool gameHasEnded = false;

    public Timer timer;

void Start()
{
    SuccessModal.SetActive(false);
    FailureModal.SetActive(false);

    Jack = Instantiate(JackPrefab, new Vector3(-7, 1, 0), Quaternion.identity);
    Blake = Instantiate(BlakePrefab, new Vector3(-7, 1, 0), Quaternion.identity);
    Instantiate(StringPrefab, new Vector3(-7, 1, 0), Quaternion.identity);

    // 선을 나타낼 GameObject 및 LineConnector 스크립트 추가
    GameObject lineConnectorGameObject = new GameObject("LineConnector");
    LineConnector lineConnector = lineConnectorGameObject.AddComponent<LineConnector>();

    // 선의 양 끝에 있는 캐릭터 설정
    lineConnector.Jack = Jack.transform;
    lineConnector.Blake = Blake.transform;
}




    void Update()
    {
        ShowModal();
    }

    void ShowModal()
{
    if (Jack != null && Blake != null)  
    {
        if (timer.isTimeOver == false && hasReachedDoor)
        {
            SuccessModal.SetActive(true);
        }

        if (Jack.transform.position.y < -5f || Blake.transform.position.y < -5f || timer.isTimeOver == true)
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
