using UnityEngine;
using UnityEngine.UI;

public class StageButtonManagerTwo : MonoBehaviour
{
    public Button[] stageButtons; // Reference to an array of UI Buttons
    private StageButtonUnlockerTwo stageButtonUnlockertwo;

    public Color uninteractableColor = new Color(0.75f, 0.75f, 0.75f); // Set the desired color for uninteractable buttons

    private void Awake()
    {
        stageButtonUnlockertwo = FindObjectOfType<StageButtonUnlockerTwo>();
    }

    private void Start()
    {
        foreach (Button stageButton in stageButtons)
        {
            stageButton.interactable = false; // Start the button as uninteractable
            SetButtonImageColor(stageButton, uninteractableColor); // Set the color for uninteractable buttons
        }

        for (int i = 0; i < stageButtons.Length && i < stageButtonUnlockertwo.stageIsUnlocked.Length; i++)
        {
            int stageNumber = i + 2; // Adjust for stages starting from 2
            if (stageButtonUnlockertwo.stageIsUnlocked[i])
            {
                stageButtons[i].interactable = true; // Unlock the button if the corresponding stage is unlocked
                SetButtonImageColor(stageButtons[i], Color.white); // Optionally set a different color for interactable buttons here if needed.
            }
        }
    }

    private void SetButtonImageColor(Button button, Color color)
    {
        Image image = button.GetComponent<Image>();
        if (image != null)
        {
            image.color = color;
        }
    }
}
