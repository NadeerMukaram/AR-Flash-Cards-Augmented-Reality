using UnityEngine;

public class SingletonManagerThree : MonoBehaviour
{
    private static SingletonManagerThree instancethree;

    private void Awake()
    {
        // Ensure there's only one instance of this script
        if (instancethree == null)
        {
            instancethree = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // If an instance already exists in another scene, destroy this one
            Destroy(gameObject);
        }
    }

    // You can add other initialization code or variables as needed
}
