using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections; // Add this line

public class BackgroundMusicController : MonoBehaviour
{
    public static BackgroundMusicController instance;
    public AudioClip[] backgroundMusicList; // Array of audio clips
    private AudioSource audioPlayer; // Change the variable name
    private int currentTrackIndex = 0; // Index of the current track
    public bool isMusicPlaying = true;

    public float timeBetweenTracks = 2.0f; // Time delay between tracks

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        audioPlayer = GetComponent<AudioSource>(); // Change the variable name

        // Set up the initial background music track
        ChangeBackgroundMusic(currentTrackIndex);

        // Play or pause the music based on the initial value of isMusicPlaying
        ToggleMusic();

        // Start playing tracks automatically
        StartCoroutine(PlayTracksAutomatically());
    }

    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Check if the current scene is one of the specified stages
        if (scene.name == "AR_Flash_Cards" || scene.name == "Quiz_Time")
        {
            // Lower the volume to 0.2
            audioPlayer.volume = 0.3f;
        }
        else
        {
            // Reset the volume to its original value
            audioPlayer.volume = 1.0f;
        }
    }

    public void ToggleMusic()
    {
        isMusicPlaying = !isMusicPlaying;

        if (isMusicPlaying)
        {
            audioPlayer.Play();
        }
        else
        {
            audioPlayer.Pause();
        }
    }

    private void PlayNextTrack()
    {
        // Increment the track index
        currentTrackIndex = (currentTrackIndex + 1) % backgroundMusicList.Length;

        // Change and play the next background music track
        ChangeBackgroundMusic(currentTrackIndex);

        // If music is supposed to be playing, play the new track
        if (isMusicPlaying)
        {
            audioPlayer.Play();
        }
    }

    private void ChangeBackgroundMusic(int trackIndex)
    {
        // Set the new audio clip
        audioPlayer.clip = backgroundMusicList[trackIndex];
    }

    private IEnumerator PlayTracksAutomatically()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeBetweenTracks);

            // Play the next track automatically
            PlayNextTrack();
        }
    }
}
