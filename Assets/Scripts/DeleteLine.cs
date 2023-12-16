using UnityEngine;
using System.Linq;

public class DeleteLine : MonoBehaviour
{

    public void DeleteObjects()
    {
        // Find all game objects with the name "Line(Clone)"
        GameObject[] allObjects = GameObject.FindObjectsOfType<GameObject>();
        GameObject[] lines = allObjects.Where(obj => obj.name == "Line(Clone)").ToArray();

        // Loop through each object and destroy it
        foreach (GameObject line in lines)
        {
            Destroy(line);
        }
    }
}
