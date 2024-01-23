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
        Blake = Instantiate(BlakePrefab, new Vector3(-7, 1, 0), Quaternion.identity);
        Jack = Instantiate(JackPrefab, new Vector3(-7, 1, 0), Quaternion.identity);
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
            // Jack과 Blake이 모두 문에 도달했을 때
            if (!timer.IsTimeOver && CheckDoorReached(Jack) && CheckDoorReached(Blake))
            {
                completionTime = timer.CompletionTime;
                successSoundEffect.Play();

                // 두 캐릭터가 문에 도달하면 성공 모달 표시
                HandleGameEnd(true); // 성공 상태로 게임 종료 처리
            }

            if (!gameHasEnded && (Jack.transform.position.y < -5f || Blake.transform.position.y < -5f || timer.IsTimeOver))
            {
                // 실패 모달 표시
                deathSoundEffect.Play();
                HandleGameEnd(false); // 실패 상태로 게임 종료 처리
                
                 
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
                // 성공한 경우 성공 모달 표시
                ShowSuccessModal(completionTime);
            }
            else
            {
                // 실패한 경우 실패 모달 표시
                ShowFailureModal();
                Invoke("RestartGame", restartDelay);
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
        // 이 부분은 문과 충돌 여부를 확인할 때 사용하는 코드입니다.
        if (collision.CompareTag("Door"))
        {
            // 문에 도달하면 해당 캐릭터를 천천히 사라지게 함
            StartCoroutine(FadeOutCharacter(collision.gameObject, 2f));
        }
    }

    bool CheckDoorReached(GameObject character)
    {
        GameObject door = GameObject.FindGameObjectWithTag("Door");
        
        if (door != null)
        {
            // 캐릭터와 문 사이의 거리를 측정하여 문에 도달했는지 여부를 판단합니다.
            float distanceToDoor = Vector2.Distance(character.transform.position, door.transform.position);
            
            return distanceToDoor < 1.6f; // 예시로 1.0f라는 거리 내에 있다면 문에 도달했다고 판단합니다. 조건을 적절히 수정하세요.
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



    // 캐릭터를 페이드 아웃하는 코루틴
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
