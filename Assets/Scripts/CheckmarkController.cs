using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CheckmarkController : MonoBehaviour
{
    public int audioIndex;

    private Button button;
    public TextMeshProUGUI statusText;

    private void Start()
    {
        button = GetComponent<Button>();

        // Add this line to get the TextMeshProUGUI component
        statusText = GetComponentInChildren<TextMeshProUGUI>();

        AudioManager audioManager = FindObjectOfType<AudioManager>(); // Find AudioManager in the scene
        if (audioManager != null)
        {
            // Get the audio status from AudioManager
            bool audioStatus = audioManager.GetAudioStatus(audioIndex);

            // Set button interactability based on the audio status
            button.interactable = audioStatus;

            // Set the status text based on button interactability
            statusText.text = button.interactable ? "Learned" : "Not Learned";
        }
        else
        {
            Debug.LogError("AudioManager not found in the scene.");
        }
    }
}
