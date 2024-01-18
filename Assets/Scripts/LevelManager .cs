using UnityEngine;

public class LevelManager : MonoBehaviour
{
    // Called when a level is passed
    public void LevelPassed(int nextLevel)
    {
        PlayerData playerData = new PlayerData();
        playerData.currentLevel = nextLevel;

        // 다음 레벨이 더 크면 highestPassedLevel 업데이트
        if (nextLevel > playerData.highestPassedLevel)
        {
            playerData.highestPassedLevel = nextLevel;
        }

        SaveSystem.SavePlayerData(playerData);
    }
}
