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
    public Timer timer;

    private GameObject Jack;
    private GameObject Blake;

    private bool hasReachedDoor = false;
    private bool gameHasEnded = false;

    public float restartDelay = 2f;

    // Fix: Declare completionTime
    private float completionTime;

    void Start()
    {
        SuccessModal.SetActive(false);
        FailureModal.SetActive(false);

        InitializeCharacters();
        InitializeLineConnector();

        PlayerData loadedData = SaveSystem.LoadPlayerData();

        if (loadedData != null)
        {
            int currentLevel = loadedData.currentLevel;
            Debug.Log("Loaded level: " + currentLevel);
        }
        else
        {
            PlayerData initialData = new PlayerData();
            SaveSystem.SavePlayerData(initialData);
        }
    }

    void Update()
    {
        ShowModal();
    }

    void InitializeCharacters()
    {
        Jack = Instantiate(JackPrefab, new Vector3(-7, 1, 0), Quaternion.identity);
        Blake = Instantiate(BlakePrefab, new Vector3(-7, 1, 0), Quaternion.identity);
    }

    void InitializeLineConnector()
    {
        GameObject lineConnectorGameObject = new GameObject("LineConnector");
        LineConnector lineConnector = lineConnectorGameObject.AddComponent<LineConnector>();

        lineConnector.Jack = Jack.transform;
        lineConnector.Blake = Blake.transform;
    }

    void ShowModal()
    {
        if (Jack != null && Blake != null)
        {
            if (!timer.IsTimeOver && hasReachedDoor)
            {
                completionTime = timer.CompletionTime;

                SuccessModal.SetActive(true);
                SuccessModal.GetComponentInChildren<Text>().text = "Success!\nTime: " + completionTime.ToString("F2") + " seconds";
            }

            if (Jack.transform.position.y < -5f || Blake.transform.position.y < -5f || timer.IsTimeOver)
            {
                FailureModal.SetActive(true);
                player.enabled = false;

                if (!gameHasEnded)
                {
                    gameHasEnded = true;
                    Invoke("RestartGame", restartDelay);
                }
            }
        }
    }

    void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "door")
        {
            hasReachedDoor = true;
        }
    }
}
