using UnityEngine;
using UnityEngine.UI;
public class CollisionHandler : MonoBehaviour
{

    public GameObject stageButton;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the collision involves an object with the "Player" tag
        if (collision.gameObject.CompareTag("Player"))
        {
            // Perform your desired action here, for example:
            Debug.Log("Nice!");

            stageButton.SetActive(true);

        }
    }
}
