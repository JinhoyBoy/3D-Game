using UnityEngine;

public class ObstaclePush : MonoBehaviour
{
    [SerializeField]
    private float forceMagnitude = 0.1f;

    private Rigidbody playerRigidbody;

    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody rigidBody = hit.collider.attachedRigidbody;

        // Apply force to the obstacle
        if (rigidBody != null)
        {
            Vector3 forceDirection = hit.transform.position - transform.position;
            forceDirection.y = 0;
            forceDirection.Normalize();

            rigidBody.AddForceAtPosition(forceDirection * forceMagnitude, transform.position, ForceMode.Impulse);
        }
    }
}

