using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.IO;

public class StarUIManager : MonoBehaviour
{
    public List<Button> letterButtons;
    public GameObject starPrefab;

    private void Start()
    {
        string jsonPath = Path.Combine(Application.persistentDataPath, "playerScoresLetters.json");

        if (File.Exists(jsonPath))
        {
            string jsonString = File.ReadAllText(jsonPath);
            StarDataList starDataList = JsonUtility.FromJson<StarDataList>(jsonString);

            for (int i = 0; i < letterButtons.Count; i++)
            {
                char letter;
                string sceneName = SceneManager.GetActiveScene().name; // Get the active scene name

                // Check the scene name for specific conditions
                if (sceneName == "Small Letters")
                {
                    letter = (char)('A' + i);
                    SetStarsForButton(letterButtons[i], GetStarsForScene($"SmallLetter-{letter}", starDataList));
                }
                else if (sceneName == "Numbers")
                {
                    letter = (char)('0' + i); // Use numbers '0' to '9'
                    SetStarsForButton(letterButtons[i], GetStarsForScene($"Number-{letter}", starDataList));
                }
                else
                {
                    letter = (char)('A' + i);
                    SetStarsForButton(letterButtons[i], GetStarsForScene($"Letter-{letter}", starDataList));
                }
            }
        }
        else
        {
            Debug.LogError("JSON file not found at: " + jsonPath);
        }
    }

    private void SetStarsForButton(Button button, int starCount)
    {
        float buttonWidth = button.GetComponent<RectTransform>().rect.width;
        float spacing = 70f;

        for (int i = 0; i < starCount; i++)
        {
            GameObject star = Instantiate(starPrefab, button.transform);
            float xPos = -(starCount - 1) * 0.5f * spacing + i * spacing;
            star.transform.localPosition = new Vector3(xPos, -50, 0);
        }
    }

    private int GetStarsForScene(string sceneName, StarDataList starDataList)
    {
        foreach (StarData starData in starDataList.scores)
        {
            // Check if the sceneName matches
            if (starData.SceneName == sceneName)
            {
                return starData.Stars;
            }
        }

        return 0;
    }
}

[System.Serializable]
public class StarData
{
    public string SceneName;
    public int Stars;
}

[System.Serializable]
public class StarDataList
{
    public List<StarData> scores;
}
