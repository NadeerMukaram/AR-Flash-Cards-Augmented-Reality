using UnityEngine;
using DG.Tweening;

public class AlphabetIntro : MonoBehaviour
{
    public Transform[] letterObjects; // Add your letter objects (sprites, images, etc.) to this array
    public float animationDuration = 1f;
    public float delayBetweenLetters = 0.2f;

    void Start()
    {
        PlayAlphabetIntro();
    }

    void PlayAlphabetIntro()
    {
        foreach (Transform letterObject in letterObjects)
        {
            // Dotween animation
            letterObject.localScale = Vector3.zero; // Set initial scale to zero

            // Dotween animation
            letterObject.DOScale(Vector3.one, animationDuration)
                .SetEase(Ease.OutBounce) // You can change the ease type
                .SetDelay(delayBetweenLetters);
        }
    }
}
