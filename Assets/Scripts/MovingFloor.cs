using UnityEngine;

public class MovingFloor : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float rotationSpeed = 45f;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0f,0f,rotationSpeed * Time.deltaTime);
    }
}
