using UnityEngine;
using UnityEngine.UI;

public class MusicToggle : MonoBehaviour
{
    public Toggle musicToggle;
    public GameObject volumeMute;

    private void Start()
    {

        // Set the initial toggle state based on the background music
        musicToggle.isOn = BackgroundMusicController.instance.isMusicPlaying;

        // Initially, hide the volume mute icon if the toggle is ON
        volumeMute.SetActive(!musicToggle.isOn);
    }

    private void Update()
    {
        // Check if the toggle is OFF, and update the visibility of the volume mute icon
        volumeMute.SetActive(!musicToggle.isOn);
    }

    public void ToggleBackgroundMusic()
    {
        BackgroundMusicController.instance.ToggleMusic();
    }
}
