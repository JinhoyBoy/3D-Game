using UnityEngine;

public class MoveObject : MonoBehaviour
{
    [SerializeField]
    private float moveDistance = 2f; // Distance to move back and forth
    [SerializeField]
    private float moveSpeed = 1f;    // Speed of movement

    private Vector3 startPosition;
    private Vector3 targetPosition;
    private bool movingForward = true;

    private void Start()
    {
        // Store the starting position
        startPosition = transform.position;
        // Calculate the target position in the forward direction
        targetPosition = startPosition + transform.forward * moveDistance;
    }

    private void Update()
    {
        // Calculate the target position based on the current movement direction
        Vector3 destination = movingForward ? targetPosition : startPosition;

        // Move towards the target position
        transform.position = Vector3.MoveTowards(transform.position, destination, moveSpeed * Time.deltaTime);

        // Check if we've reached the destination
        if (Vector3.Distance(transform.position, destination) < 0.01f)
        {
            // Toggle movement direction
            movingForward = !movingForward;
        }
    }
}
