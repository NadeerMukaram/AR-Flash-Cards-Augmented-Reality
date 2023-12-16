using UnityEngine;
using System.IO;

public class DisplayObjectController : MonoBehaviour
{
    public GameObject displayObject;
    private string jsonFilePath;

    private void Awake()
    {
        // Set the JSON file path
        jsonFilePath = Path.Combine(Application.persistentDataPath, "animalLearnedCount.json");
    }

    private void Start()
    {
        UpdateDisplayObjectVisibility();
    }

    private void UpdateDisplayObjectVisibility()
    {
        if (File.Exists(jsonFilePath))
        {
            string jsonData = File.ReadAllText(jsonFilePath);
            TrueCountData trueCountData = JsonUtility.FromJson<TrueCountData>(jsonData);

            if (trueCountData.TrueCount >= 26)
            {
                DisplayObject(true);
            }
            else
            {
                DisplayObject(false);
            }
        }
        else
        {
            DisplayObject(false);
        }
    }

    private void DisplayObject(bool isActive)
    {
        if (displayObject != null)
        {
            displayObject.SetActive(isActive);
        }
    }

    [System.Serializable]
    private class TrueCountData
    {
        public int TrueCount;
    }
}
