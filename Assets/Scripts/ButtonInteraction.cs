using UnityEngine;
using UnityEngine.UI;

public class ButtonInteraction : MonoBehaviour
{
    public Button stage2;
    public Button stage3;

    public static ButtonInteraction Instance;

    private void Awake()
    {
        //if (Instance == null)
        //{
        //    Instance = this;
        //    DontDestroyOnLoad(gameObject);
        //}
        //else
        //{
        //    Destroy(gameObject);
        //}
    }

    private void Start()
    {
        // Set the initial interactable state based on the 'stage2Interactable' variable stored in PlayerPrefs
        bool stage2Interactable = PlayerPrefs.GetInt("stage2Interactable", 0) == 1;
        stage2.interactable = stage2Interactable;

        // Set the initial interactable state for stage 3 based on the 'stage3Interactable' variable stored in PlayerPrefs
        bool stage3Interactable = PlayerPrefs.GetInt("stage3Interactable", 0) == 1;
        stage3.interactable = stage3Interactable;
    }

    public void SetInteractable(bool interactable, string interactableKey)
    {
        // Set the specified interactable variable and store it in PlayerPrefs
        int interactableValue = interactable ? 1 : 0;
        PlayerPrefs.SetInt(interactableKey, interactableValue);
        PlayerPrefs.Save();

        // Update the button's interactable state based on the key
        if (interactableKey == "stage2Interactable")
        {
            stage2.interactable = interactable;
        }
        else if (interactableKey == "stage3Interactable")
        {
            stage3.interactable = interactable;
        }
    }
}
