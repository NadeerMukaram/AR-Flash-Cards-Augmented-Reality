using UnityEngine;

public class UnlockStage : MonoBehaviour
{
    [SerializeField]
    private StageButtonUnlocker stageButtonUnlocker;

    void Start()
    {
        if (stageButtonUnlocker == null)
        {
            Debug.LogError("StageButtonUnlocker not found. Please assign it in the Unity Editor.");
        }
    }

    public void UnlockStageNumber(int stageNumber)
    {
        if (stageButtonUnlocker != null)
        {
            stageButtonUnlocker.UnlockStage(stageNumber);
        }
    }
}
