using System.Collections;
using UnityEngine;

public class CircleCollisionChecker : MonoBehaviour
{
    public GameObject circlePrefab;
    public int numberOfCircles = 5;
    public LayerMask collisionLayer;

    private void Start()
    {
        SpawnCircles();
    }

    private void SpawnCircles()
    {
        for (int i = 0; i < numberOfCircles; i++)
        {
            Vector2 randomPosition = new Vector2(Random.Range(-5f, 5f), Random.Range(-5f, 5f));
            GameObject circle = Instantiate(circlePrefab, randomPosition, Quaternion.identity);
            circle.GetComponent<CircleCollider2D>().isTrigger = true;
            circle.GetComponent<Rigidbody2D>().isKinematic = true;
        }
    }

    private void Update()
    {
        GameObject[] circles = GameObject.FindGameObjectsWithTag("Circle");

        if (circles.Length > 0)
        {
            bool allCirclesCollided = true;

            foreach (GameObject circle in circles)
            {
                Collider2D[] collisions = Physics2D.OverlapCircleAll(circle.transform.position, 0.5f, collisionLayer);

                if (collisions.Length == 0)
                {
                    allCirclesCollided = false;
                    break;
                }
            }

            if (allCirclesCollided)
            {
                Debug.Log("All circles have collided with the specified LayerMask!");
            }
        }
    }
}
