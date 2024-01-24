using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public Player player;
    public GameObject JackPrefab;
    public GameObject BlakePrefab;
    public Timer timer;
    public Text successText;

    private GameObject Jack;
    private GameObject Blake;

    private bool gameHasEnded = false;

    public float restartDelay = 2f;
    private float completionTime;

    public Camera mainCamera;
    public GameObject SuccessModal;
    public GameObject FailureModal;

    public Vector3 m_JakeSpawnLocation = new Vector3(-7, 1, 0);
    public Vector3 m_BlakeSpawnLocation = new Vector3(-7, 1, 0);

    [SerializeField] private AudioSource deathSoundEffect;
    [SerializeField] private AudioSource successSoundEffect;

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
        if (Blake != null && mainCamera != null)
        {
            UpdateCameraPosition();
        }

        CheckCompletionConditions();
    }

    void UpdateCameraPosition()
    {
        Vector3 playerPos = Blake.transform.position;
        mainCamera.transform.position = new Vector3(playerPos.x, playerPos.y, -10);
        Debug.Log("Camera Position: " + mainCamera.transform.position);
    }

    void InitializeCharacters()
    {
        Blake = Instantiate(BlakePrefab, m_BlakeSpawnLocation, Quaternion.identity);
        Jack = Instantiate(JackPrefab, m_JakeSpawnLocation, Quaternion.identity);
    }

    void InitializeLineConnector()
    {
        GameObject lineConnectorGameObject = new GameObject("LineConnector");
        LineConnector lineConnector = lineConnectorGameObject.AddComponent<LineConnector>();

        lineConnector.Blake = Blake.transform;
        lineConnector.Jack = Jack.transform;
    }

    void CheckCompletionConditions()
    {
        if (Jack != null && Blake != null)
        {
            // When both Jack and Blake reach the door
            if (!timer.IsTimeOver && CheckDoorReached(Jack) && CheckDoorReached(Blake))
            {
                completionTime = timer.CompletionTime;

                if (successSoundEffect != null)
                {
                    successSoundEffect.Play();
                }

                // Display success modal when both characters reach the door
                HandleGameEnd(true); // End the game in success state
            }

            if (!gameHasEnded && (Jack.transform.position.y < -5f || Blake.transform.position.y < -5f || timer.IsTimeOver))
            {
                // Display failure modal
                HandleGameEnd(false); // End the game in failure state

                if (deathSoundEffect != null)
                {
                    deathSoundEffect.Play();
                }
            }
        }
    }

    void HandleGameEnd(bool isSuccess)
    {
        if (player != null)
        {
            player.enabled = false;
        }

        if (!gameHasEnded)
        {
            gameHasEnded = true;

            if (isSuccess)
            {
                // Display success modal
                ShowSuccessModal(completionTime);
            }
            else
            {
                // Display failure modal
                ShowFailureModal();
            }
        }
    }

    void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void ShowSuccessModal(float completionTime)
    {
        StartCoroutine(DelayedFadeOutCharactersAndShowModal(completionTime));
    }

    void ShowFailureModal()
    {
        FailureModal.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // This part is used to check if the characters collide with the door
        if (collision.CompareTag("Door"))
        {
            // When reaching the door, make the character slowly disappear
            StartCoroutine(FadeOutCharacter(collision.gameObject, 2f));
        }
    }

    bool CheckDoorReached(GameObject character)
    {
        GameObject door = GameObject.FindGameObjectWithTag("Door");

        if (door != null)
        {
            // Measure the distance between the character and the door to determine if they reached the door
            float distanceToDoor = Vector2.Distance(character.transform.position, door.transform.position);

            return distanceToDoor < 1.6f; // If within a certain distance, consider reaching the door. Modify the condition appropriately.
        }
        else
        {
            Debug.LogError("Door not found. Make sure the 'Door' object has the 'Door' tag.");
            return false;
        }
    }

    IEnumerator DelayedFadeOutCharactersAndShowModal(float completionTime)
    {
        // Fade out characters
        yield return FadeOutCharacter(Blake, 0.5f);
        yield return FadeOutCharacter(Jack, 0.5f);

        // Wait for a short duration
        yield return new WaitForSeconds(0.5f);

        // Show success modal
        SuccessModal.SetActive(true);

        // Update success modal text with completion time
        successText = SuccessModal.GetComponentInChildren<Text>();
        int minutes = Mathf.FloorToInt(completionTime / 60);
        int seconds = Mathf.FloorToInt(completionTime % 60);
        successText.text = string.Format("Success!\n\nTime: " + "{0:00}:{1:00}", minutes, seconds);
    }

    // Coroutine to fade out the character
    IEnumerator FadeOutCharacter(GameObject character, float duration)
    {
        SpriteRenderer characterRenderer = character.GetComponent<SpriteRenderer>();
        Color originalColor = characterRenderer.color;
        Color targetColor = new Color(originalColor.r, originalColor.g, originalColor.b, 0f);

        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            characterRenderer.color = Color.Lerp(originalColor, targetColor, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        characterRenderer.color = targetColor;
    }
}
