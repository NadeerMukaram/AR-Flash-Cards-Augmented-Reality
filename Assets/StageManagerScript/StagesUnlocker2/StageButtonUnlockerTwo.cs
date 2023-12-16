using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageButtonUnlockerTwo : MonoBehaviour
{
    public bool[] stageIsUnlocked; // Change this based on your game logic
    private const string unlockKeyPrefix = "StageUnlockedTwo_"; // PlayerPrefs key prefix for stage unlock status

    void Start()
    {
        // Load the unlocked status from PlayerPrefs on game start
        LoadStageUnlockStatus();
    }

    private void LoadStageUnlockStatus()
    {
        stageIsUnlocked = new bool[26];

        for (int i = 0; i < stageIsUnlocked.Length; i++)
        {
            stageIsUnlocked[i] = PlayerPrefs.GetInt(unlockKeyPrefix + (i + 2), 0) == 1;
        }
    }

    public void UnlockStage(int stageNumber)
    {
        if (stageNumber >= 2 && stageNumber <= 27)
        {
            // Set stage unlock status to true and save it in PlayerPrefs
            stageIsUnlocked[stageNumber - 2] = true;
            PlayerPrefs.SetInt(unlockKeyPrefix + stageNumber, 1);
            PlayerPrefs.Save();

            Debug.Log("Stage " + stageNumber + " unlocked");
        }
        else
        {
            Debug.LogWarning("Invalid stage number: " + stageNumber);
        }

    }
}
