using UnityEngine;

public class SingletonManagerTwo : MonoBehaviour
{
    private static SingletonManagerTwo instancetwo;

    private void Awake()
    {
        // Ensure there's only one instance of this script
        if (instancetwo == null)
        {
            instancetwo = this;
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
