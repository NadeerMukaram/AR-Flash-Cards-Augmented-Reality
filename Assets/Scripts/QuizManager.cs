using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening; // Import DOTween

public class QuizManager : MonoBehaviour
{
    public Question[] questions; // Array of questions
    private int currentQuestionIndex = 0; // Index of the current question

    public Image questionImage; // Image component for displaying the question image
    public TextMeshProUGUI questionText; // TextMeshPro component for displaying the question text
    public TextMeshProUGUI correctCountText; // TextMeshPro component for displaying the correct count
    public Button aButton; // Button for 'A'
    public Button bButton; // Button for 'B'
    public Button cButton; // Button for 'C'

    public AudioSource audioSource; // AudioSource component for playing audio
    public AudioSource audioSourceForSFX; // AudioSource component for playing audio
    public AudioSource starAudio; // AudioSource component for playing star audio

    public AudioClip correctAnswerSound; // Sound for correct answer
    public AudioClip wrongAnswerSound; // Sound for wrong answer
    public AudioClip starSound; // Sound for earning a star

    private int correctCount = 0; // Count of correctly answered questions

    public GameObject star1;
    public GameObject star2;
    public GameObject star3;


    public GameObject star1Panel;
    public GameObject star2Panel;
    public GameObject star3Panel;

    public GameObject noStarPanel;

    public GameObject starScore1;
    public GameObject starScore2;
    public GameObject starScore3;

    public GameObject noScore;

    private int totalQuestions;

    private const string PlayerPrefKey = "PlayerScore";

    // Assuming you have a TextMeshProUGUI component for displaying the score
    public TextMeshProUGUI scoreText;


    void Start()
    {
        totalQuestions = Mathf.Min(questions.Length, 15); // Set the total questions to the minimum of available questions and 15
        ShuffleQuestions();
        ShowQuestion();
        DisplayPreviousScore();
    }

    void ShuffleQuestions()
    {
        // Fisher-Yates shuffle algorithm
        for (int i = totalQuestions - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            Question temp = questions[i];
            questions[i] = questions[randomIndex];
            questions[randomIndex] = temp;
        }
    }

    void ShowQuestion()
    {
        if (currentQuestionIndex < totalQuestions)
        {
            Question currentQuestion = questions[currentQuestionIndex];

            PlayAudio(currentQuestion.questionAudio);

            questionImage.sprite = currentQuestion.image;
            questionText.text = currentQuestion.text;

            aButton.GetComponentInChildren<TextMeshProUGUI>().text = currentQuestion.choices[0];
            bButton.GetComponentInChildren<TextMeshProUGUI>().text = currentQuestion.choices[1];

            if (currentQuestion.choices.Length > 2)
            {
                cButton.gameObject.SetActive(true);
                cButton.GetComponentInChildren<TextMeshProUGUI>().text = currentQuestion.choices[2];
            }
            else
            {
                cButton.gameObject.SetActive(false);
            }

            aButton.onClick.RemoveAllListeners();
            aButton.onClick.AddListener(() => Answer(0));

            bButton.onClick.RemoveAllListeners();
            bButton.onClick.AddListener(() => Answer(1));

            if (cButton.gameObject.activeSelf)
            {
                cButton.onClick.RemoveAllListeners();
                cButton.onClick.AddListener(() => Answer(2));
            }
        }
        else
        {

            aButton.gameObject.SetActive(false);
            bButton.gameObject.SetActive(false);
            cButton.gameObject.SetActive(false);

            DisplayStarRating();
        }
    }

    void Answer(int playerChoiceIndex)
    {
        Question currentQuestion = questions[currentQuestionIndex];

        if (playerChoiceIndex == currentQuestion.correctChoiceIndex)
        {
            correctCount++;
            PlayAudioSFX(correctAnswerSound);
        }
        else
        {
            PlayAudioSFX(wrongAnswerSound);
        }

        currentQuestionIndex++;
        correctCountText.text = correctCount + "/15";

        ShowQuestion();
    }

    void DisplayStarRating()
    {
        float percentageCorrect = (float)correctCount / totalQuestions * 100f;

        // Convert the percentage to stars (adjust the thresholds as needed)
        int starsEarned = 0;
        if (percentageCorrect >= 90f)
        {
            starsEarned = 3;
            ActivateStarPanel(star3Panel);

            ApplyStarTween(star3, true);
        }
        else if (percentageCorrect >= 70f)
        {
            starsEarned = 2;
            ActivateStarPanel(star2Panel);

            ApplyStarTween(star2, true);
        }
        else if (percentageCorrect >= 50f)
        {
            starsEarned = 1;
            ActivateStarPanel(star1Panel);

            ApplyStarTween(star1, true);
        }
        else
        {
            ActivateStarPanel(noStarPanel);

        }

        // Store the stars earned in PlayerPrefs
        PlayerPrefs.SetInt(PlayerPrefKey, starsEarned);
        PlayerPrefs.Save(); // Save PlayerPrefs data

        // Display the previous score with the updated stars earned
        DisplayPreviousScore();

        // Reshuffle questions for the next playthrough
        ShuffleQuestions();
    }

    void ActivateStarPanel(GameObject starPanel)
    {
        if (starPanel != null)
        {
            starPanel.SetActive(true);
        }
    }


    void PlayAudio(AudioClip audioClip)
    {
        if (audioSource != null && audioClip != null)
        {
            audioSource.clip = audioClip;
            audioSource.Play();
        }
    }

    void PlayAudioSFX(AudioClip audioClip)
    {
        if (audioSourceForSFX != null && audioClip != null)
        {
            audioSourceForSFX.clip = audioClip;
            audioSourceForSFX.Play();
        }
    }

    void ApplyStarTween(GameObject star, bool isActive)
    {
        if (isActive)
        {
            star.SetActive(true);
            star.transform.DOScale(Vector3.one * 0.5f, 0.5f).From().SetEase(Ease.OutBack);

            if (starAudio != null)
            {
                starAudio.PlayOneShot(starSound); // Play star sound
            }
        }
        else
        {
            star.transform.DOScale(Vector3.zero, 0.5f).OnComplete(() => star.SetActive(false)).SetEase(Ease.InBack);
        }
    }


    void DisplayPreviousScore()
    {
        // Retrieve the stored score from PlayerPrefs
        int storedScore = PlayerPrefs.GetInt(PlayerPrefKey, 0);

        // Display the previous score in TextMeshProUGUI
        if (scoreText != null)
        {
            scoreText.text = "Previous Stars Score: " + storedScore;
        }

        // Example: Show/hide GameObjects based on the number of stars earned
        starScore1.SetActive(false);
        starScore2.SetActive(false);
        starScore3.SetActive(false);
        noScore.SetActive(false);

        switch (storedScore)
        {
            case 1:
                starScore1.SetActive(true);
                break;
            case 2:
                starScore2.SetActive(true);
                break;
            case 3:
                starScore3.SetActive(true);
                break;
            default:
                noScore.SetActive(true);
                break;
        }
    }


}

[System.Serializable]
public class Question
{
    public Sprite image; // Question image
    public string text; // Question text
    public string[] choices; // Array of choices
    public int correctChoiceIndex; // Index of the correct choice
    public AudioClip questionAudio; // Audio for the question
}
