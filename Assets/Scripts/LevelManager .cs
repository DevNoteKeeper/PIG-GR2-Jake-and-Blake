using UnityEngine;

public class LevelManager : MonoBehaviour
{
    // Called when a level is passed
    public void LevelPassed(int nextLevel)
    {
        PlayerData playerData = new PlayerData();
        playerData.currentLevel = nextLevel;

        SaveSystem.SavePlayerData(playerData);
    }
}
