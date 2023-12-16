using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.IO;
using DG.Tweening; // Import DOTween


public class Scoring : MonoBehaviour
{
    public GameObject star1;
    public GameObject star2;
    public GameObject star3;


    public AudioSource starAudio;

    private bool isScoring = false;
    private float score = 15f;
    public TextMeshProUGUI scoreText;

    private float scoreDecreaseRate = 1f;

    public GameObject line;

    private Dictionary<string, int> scoreHistory = new Dictionary<string, int>();

    [System.Serializable]
    private class ScoreData
    {
        public string SceneName;
        public int Stars;
    }

    [System.Serializable]
    private class ScoreDataList
    {
        public List<ScoreData> scores;

        public ScoreDataList(List<ScoreData> scores)
        {
            this.scores = scores;
        }
    }


    private void Start()
    {
        StartScoring();

        if (star1 != null) star1.SetActive(false);
        if (star2 != null) star2.SetActive(false);
        if (star3 != null) star3.SetActive(false);


        // Load score history only once when the game starts
        LoadScoreHistory();

        // Initialize scoreHistory if it is null
        if (scoreHistory == null)
        {
            scoreHistory = new Dictionary<string, int>();
        }
    }


    private void Update()
    {
        if (isScoring)
        {
            UpdateScore();
        }
    }

    private void StartScoring()
    {
        isScoring = true;
        InvokeRepeating("UpdateScore", 1f, 1f);
    }

    private void StopScoring()
    {
        isScoring = false;
        CancelInvoke("UpdateScore");
        Destroy(line);

        UpdateStars();
        SaveScoreToJSON();
    }

    private void UpdateScore()
    {
        GameObject nextObject = GameObject.Find("Next");
        if (nextObject != null && nextObject.activeSelf)
        {
            StopScoring();
            return;
        }

        score -= scoreDecreaseRate * Time.deltaTime;
        score = Mathf.Max(score, 0f);

        UpdateScoreUI();
    }

    private void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = Mathf.Round(score).ToString();
        }
    }

    private void UpdateStars()
    {
        int stars = CalculateStars();

        if (star1 != null)
        {
            ApplyStarTween(star1, stars >= 1);
        }

        if (star3 != null)
        {
            ApplyStarTween(star3, stars >= 2);
        }

        if (star2 != null)
        {
            ApplyStarTween(star2, stars >= 3);
        }
    }

    private void ApplyStarTween(GameObject star, bool isActive)
    {
        if (isActive)
        {
            star.SetActive(true);
            // Use DOTween to create a scale animation for the star
            star.transform.DOScale(Vector3.one * 0.5f, 0.5f).From().SetEase(Ease.OutBack);

            // Play the star sound when it becomes active
            if (starAudio != null)
            {
                starAudio.Play();
            }
        }
        else
        {
            // Use DOTween to create a scale animation for hiding the star
            star.transform.DOScale(Vector3.zero, 0.5f).OnComplete(() => star.SetActive(false)).SetEase(Ease.InBack);
        }
    }

    private int CalculateStars()
    {
        int stars = 0;
        if (score >= 10f)
        {

            stars = 3;
        }
        else if (score >= 5f)
        {

            stars = 2;
        }
        else if (score >= 0f)
        {

            stars = 1;
        }

        return stars;
    }

    private void SaveScoreToJSON()
    {
        // Create a data structure to store the scene name and stars
        string sceneName = SceneManager.GetActiveScene().name;
        int newStars = CalculateStars();

        // Load existing score data from the file
        LoadScoreHistory();

        // Initialize scoreHistory if it is null
        if (scoreHistory == null)
        {
            scoreHistory = new Dictionary<string, int>();
        }

        // Check if the scene is already in the score history
        if (scoreHistory.TryGetValue(sceneName, out int existingStars))
        {
            // If the existing score is greater than or equal to the new score, do not update
            if (existingStars >= newStars)
            {
                Debug.Log("Existing score is higher or equal. Not updating.");
                return;
            }
        }

        // Update or add the new score entry to the dictionary
        scoreHistory[sceneName] = newStars;

        // Convert the dictionary to a list for appending
        List<ScoreData> scoreList = new List<ScoreData>();
        foreach (var entry in scoreHistory)
        {
            scoreList.Add(new ScoreData { SceneName = entry.Key, Stars = entry.Value });
        }

        // Convert the list to JSON using JsonUtility
        string jsonData = JsonUtility.ToJson(new ScoreDataList(scoreList));

        // Specify the path for the JSON file
        string jsonFilePath = Path.Combine(Application.persistentDataPath, "playerScoresLetters.json");

        // Write the updated JSON data to the file
        File.WriteAllText(jsonFilePath, jsonData);

        Debug.Log("Score history saved to: " + jsonFilePath);
    }



    private void LoadScoreHistory()
    {
        // Construct the file path using Application.persistentDataPath
        string jsonFilePath = Path.Combine(Application.persistentDataPath, "playerScoresLetters.json");

        // Check if the file exists before attempting to load it
        if (File.Exists(jsonFilePath))
        {
            // Read the JSON data from the file
            string jsonData = File.ReadAllText(jsonFilePath);

            // Deserialize the JSON data using JsonUtility
            ScoreDataList scoreDataList = JsonUtility.FromJson<ScoreDataList>(jsonData);

            // Check if the deserialization was successful and the list is not null
            if (scoreDataList != null && scoreDataList.scores != null)
            {
                // Extract scores from the deserialized list
                foreach (var scoreData in scoreDataList.scores)
                {
                    scoreHistory[scoreData.SceneName] = scoreData.Stars;
                }
            }
            else
            {
                // Initialize scoreHistory if deserialization fails
                scoreHistory = new Dictionary<string, int>();
            }
        }
        else
        {
            // Initialize scoreHistory if the file doesn't exist
            scoreHistory = new Dictionary<string, int>();
        }
    }


    [System.Serializable]
    private class ScoreDataDictionary
    {
        public Dictionary<string, int> scores;

        public ScoreDataDictionary(Dictionary<string, int> scores)
        {
            this.scores = scores;
        }
    }
}
