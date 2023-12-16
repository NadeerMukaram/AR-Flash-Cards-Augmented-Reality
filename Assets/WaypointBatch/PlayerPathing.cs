using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;

public class PlayerPathing : MonoBehaviour
{
    public Transform[] targetTransforms;

    public GameObject nextScene;

    private void OnTriggerEnter2D(Collider2D other)
    {
        for (int i = 0; i < targetTransforms.Length; i++)
        {
            if (targetTransforms[i] != null && other.transform == targetTransforms[i])
            {
                Debug.Log("Collided with target transform " + i);

                // Destroy the GameObject associated with the detected target transform
                Destroy(targetTransforms[i].gameObject);

                // Set the target transform to null to mark it as destroyed
                targetTransforms[i] = null;
            }
        }

        // Check if all targets have been destroyed
        if (AllTargetsDestroyed())
        {
            Debug.Log("All targets destroyed!");

            string currentSceneName = SceneManager.GetActiveScene().name;

            // Check if the scene name starts with "Letter-"
            if (currentSceneName.StartsWith("Letter-"))
            {
                UnlockStagesBasedOnScene_BigLetters();
            }
            // Check if the scene name starts with "SmallLetter-"
            else if (currentSceneName.StartsWith("SmallLetter-"))
            {
                UnlockStagesBasedOnScene_SmallLetters();
            }
            // Check if the scene name starts with "Number-"
            else if (currentSceneName.StartsWith("Number-"))
            {
                UnlockStagesBasedOnScene_Numbers();
            }
            else
            {
                Debug.LogWarning("Scene name does not match expected patterns for unlocking stages.");
            }

            // Activate the next scene GameObject for scene transition.
            if (nextScene != null)
            {
                nextScene.SetActive(true);
            }
        }

    }

    private bool AllTargetsDestroyed()
    {
        // Check if all elements in the array are null
        foreach (Transform targetTransform in targetTransforms)
        {
            if (targetTransform != null)
            {
                return false;
            }
        }
        return true;
    }

    private void UnlockStagesBasedOnScene_BigLetters()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        StageButtonUnlocker stageButtonUnlocker = FindObjectOfType<StageButtonUnlocker>();

        if (stageButtonUnlocker != null)
        {
            // Scene Name e.g Letter-A
            int stageNumber = currentSceneName[currentSceneName.Length - 1] - 'A' + 2;

            if (stageNumber >= 2 && stageNumber <= 27)
            {
                stageButtonUnlocker.UnlockStage(stageNumber);
            }
            else
            {
                Debug.LogWarning("Invalid stage number for scene: " + currentSceneName);
            }
        }
        else
        {
            Debug.LogError("StageButtonUnlocker not found. Please ensure it exists in the scene.");
        }
    }


    private void UnlockStagesBasedOnScene_SmallLetters()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        StageButtonUnlockerTwo stageButtonUnlocker = FindObjectOfType<StageButtonUnlockerTwo>();

        if (stageButtonUnlocker != null)
        {
            // Scene Name with this script, e.g SmallLetter-A
            int stageNumber = currentSceneName[currentSceneName.Length - 1] - 'A' + 2;

            if (stageNumber >= 2 && stageNumber <= 27)
            {
                stageButtonUnlocker.UnlockStage(stageNumber);
            }
            else
            {
                Debug.LogWarning("Invalid stage number for scene: " + currentSceneName);
            }
        }
        else
        {
            Debug.LogError("StageButtonUnlocker not found. Please ensure it exists in the scene.");
        }
    }


    private void UnlockStagesBasedOnScene_Numbers()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        StageButtonUnlockerThree stageButtonUnlocker = FindObjectOfType<StageButtonUnlockerThree>();

        if (stageButtonUnlocker != null)
        {
            // Scene Name with this script, e.g Number-1
            int stageNumber = currentSceneName[currentSceneName.Length - 1] - '0' + 2;

            if (stageNumber >= 2 && stageNumber <= 27)
            {
                stageButtonUnlocker.UnlockStage(stageNumber);
            }
            else
            {
                Debug.LogWarning("Invalid stage number for scene: " + currentSceneName);
            }
        }
        else
        {
            Debug.LogError("StageButtonUnlocker not found. Please ensure it exists in the scene.");
        }
    }
}
