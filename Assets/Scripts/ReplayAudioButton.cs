using UnityEngine;

public class ReplayAudioButton : MonoBehaviour
{
    public AudioSource audioSource; // Reference to the AudioSource you want to replay

    void Start()
    {
        // Ensure that the AudioSource component is assigned
        if (audioSource == null)
        {
            Debug.LogError("AudioSource not assigned to ReplayAudioButton script!");
            gameObject.SetActive(false); // Disable the button if there's no AudioSource
        }
    }

    public void ReplayAudio()
    {
        // Check if the audio source and clip are set
        if (audioSource != null && audioSource.clip != null)
        {
            audioSource.Stop(); // Stop any currently playing audio
            audioSource.Play(); // Replay the audio clip
        }
    }
}
