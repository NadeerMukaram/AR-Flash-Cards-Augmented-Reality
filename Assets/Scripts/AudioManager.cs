using UnityEngine;
using TMPro;
using System.IO;

public class AudioManager : MonoBehaviour
{
    public AudioClip[] audioClips;
    public bool[] audioStatus;

    public TextMeshProUGUI countText;
    public GameObject displayObject;
    public GameObject animalQuiz;

    private string jsonFilePath;

    private void Awake()
    {
        // Initialize the audio status array
        audioStatus = new bool[audioClips.Length];

        // Load saved audio status from PlayerPrefs
        LoadAudioStatus();

        // Set the JSON file path
        jsonFilePath = Path.Combine(Application.persistentDataPath, "animalLearnedCount.json");
    }

    private void Update()
    {
        UpdateCountText();
    }

    public void PlayAudioByIndex(int index)
    {
        if (index >= 0 && index < audioClips.Length)
        {
            AudioSource.PlayClipAtPoint(audioClips[index], transform.position);
            audioStatus[index] = true;
            SaveAudioStatus();
            UpdateCountText();
        }
        else
        {
            Debug.LogWarning("Invalid audio index!");
        }
    }

    public bool GetAudioStatus(int index)
    {
        if (index >= 0 && index < audioStatus.Length)
        {
            return audioStatus[index];
        }
        else
        {
            Debug.LogWarning("Invalid audio index!");
            return false;
        }
    }

    public void SaveAudioStatus()
    {
        for (int i = 0; i < audioStatus.Length; i++)
        {
            PlayerPrefs.SetInt("AudioStatus_" + i, audioStatus[i] ? 1 : 0);
        }
        PlayerPrefs.Save();

        // Save trueCount to JSON file
        int trueCount = CalculateTrueCount();
        SaveTrueCountToJson(trueCount);
    }

    private void SaveTrueCountToJson(int trueCount)
    {
        TrueCountData trueCountData = new TrueCountData { TrueCount = trueCount };
        string jsonData = JsonUtility.ToJson(trueCountData);
        File.WriteAllText(jsonFilePath, jsonData);
    }

    private void LoadAudioStatus()
    {
        for (int i = 0; i < audioStatus.Length; i++)
        {
            audioStatus[i] = PlayerPrefs.GetInt("AudioStatus_" + i, 0) == 1;
        }
        UpdateCountText();

        // Load trueCount from JSON file
        int loadedTrueCount = LoadTrueCountFromJson();
        // Use loadedTrueCount as needed
    }

    private int LoadTrueCountFromJson()
    {
        if (File.Exists(jsonFilePath))
        {
            string jsonData = File.ReadAllText(jsonFilePath);
            TrueCountData trueCountData = JsonUtility.FromJson<TrueCountData>(jsonData);
            return trueCountData.TrueCount;
        }
        return 0;
    }

    private int CalculateTrueCount()
    {
        int trueCount = 0;
        foreach (bool status in audioStatus)
        {
            if (status)
                trueCount++;
        }
        return trueCount;
    }

    public void UpdateCountText()
    {
        int trueCount = CalculateTrueCount();

        if (countText != null)
        {
            countText.text = trueCount + "/" + audioStatus.Length;

            if (trueCount >= 26)
            {
                animalQuiz.SetActive(true);

                if (!PlayerPrefs.HasKey("DisplayObjectShown"))
                {
                    if (displayObject != null)
                    {
                        displayObject.SetActive(true);
                        PlayerPrefs.SetInt("DisplayObjectShown", 1);
                        PlayerPrefs.Save();
                    }
                }
            }
        }
    }

    // Class to store trueCount data for JSON serialization
    [System.Serializable]
    private class TrueCountData
    {
        public int TrueCount;
    }
}
