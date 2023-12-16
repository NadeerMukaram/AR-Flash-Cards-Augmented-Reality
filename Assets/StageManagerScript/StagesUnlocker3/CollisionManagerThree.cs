using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;

public class CollisionManagerThree : MonoBehaviour
{
    [System.Serializable]
    public class WaypointBatch
    {
        public Transform[] waypoints;
    }

    public WaypointBatch[] waypointBatches;
    private int currentBatchIndex = 0;
    private List<Transform> visitedWaypoints = new List<Transform>();

    public GameObject nextScene;

    public bool letterComplete = false;

    private void Start()
    {
        SetBatchActive(currentBatchIndex);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (currentBatchIndex < waypointBatches.Length)
        {
            WaypointBatch currentBatch = waypointBatches[currentBatchIndex];

            // Check if the colliding object is one of the waypoints in the current batch.
            if (ArrayContainsTransform(other.transform, currentBatch.waypoints) && !visitedWaypoints.Contains(other.transform))
            {
                visitedWaypoints.Add(other.transform);
                Debug.Log("Player visited waypoint: " + other.gameObject.name);

                // Immediately hide the collided waypoint.
                SetWaypointActive(other.transform, false);

                // Check if the player has visited all waypoints in the current batch.
                if (visitedWaypoints.Count == currentBatch.waypoints.Length)
                {
                    Debug.Log("Player completed batch " + (currentBatchIndex + 1));

                    currentBatchIndex++;

                    // Check if there are more batches of waypoints.
                    if (currentBatchIndex < waypointBatches.Length)
                    {
                        // Show the waypoints of the next batch.
                        SetBatchActive(currentBatchIndex);
                    }
                    else
                    {
                        // Perform your action when all batches are completed.
                        Debug.Log("Player completed all batches.");

                        letterComplete = true;

                        // Activate the next scene GameObject for scene transition.
                        if (nextScene != null)
                        {
                            nextScene.SetActive(true);
                        }

                        // Unlock stages based on the current scene name
                        UnlockStagesBasedOnScene();
                    }

                    visitedWaypoints.Clear();
                }
            }
        }
    }

    private void UnlockStagesBasedOnScene()
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

    private void SetWaypointActive(Transform waypoint, bool active = true)
    {
        waypoint.gameObject.SetActive(active);
    }

    private void SetBatchActive(int batchIndex)
    {
        if (batchIndex >= 0 && batchIndex < waypointBatches.Length)
        {
            Transform[] waypoints = waypointBatches[batchIndex].waypoints;

            foreach (Transform waypoint in waypoints)
            {
                SetWaypointActive(waypoint, true);
            }
        }
    }

    private bool ArrayContainsTransform(Transform obj, Transform[] objArray)
    {
        return objArray.Contains(obj);
    }
}
