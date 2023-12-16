using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    public int audioIndexToPlay;
    public AudioManager audioManager;
    private AudioSource audioSource; // Add this line

    private void Start()
    {
        audioSource = GetComponent<AudioSource>(); // Add this line
    }

    public void PlayAudioOnClick()
    {
        if (audioManager != null)
        {
            // Play audio using the AudioSource component
            if (audioIndexToPlay >= 0 && audioIndexToPlay < audioManager.audioClips.Length)
            {
                audioSource.PlayOneShot(audioManager.audioClips[audioIndexToPlay]);
                audioManager.audioStatus[audioIndexToPlay] = true;
                audioManager.SaveAudioStatus();
                audioManager.UpdateCountText();
            }
            else
            {
                Debug.LogWarning("Invalid audio index!");
            }
        }
        else
        {
            Debug.LogError("AudioManager reference not set in the Unity Editor.");
        }
    }
}
