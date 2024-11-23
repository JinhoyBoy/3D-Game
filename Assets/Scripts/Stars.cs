using UnityEngine;
using UnityEngine.UI;

public class Stars : MonoBehaviour
{
    [SerializeField] private Image starImage;
    [SerializeField] private int timeForRemovingStar; // Time in seconds to remove a star
    private int michelinStarCount = 3; // Starting star count
    private float elapsedTime; // Timer to track time passed

    void Start()
    {
        michelinStarCount = 3;
        elapsedTime = 0f; 
    }

    void Update()
    {
        elapsedTime += Time.deltaTime;

        // Check if enough time has passed to remove a star
        if (elapsedTime >= timeForRemovingStar && michelinStarCount > 0)
        {
            michelinStarCount -= 1;
            elapsedTime = 0f; // Reset the timer

            // Adjust star opacity for the remaining stars
            starImage.color = new Color(1, 1, 1, 0.4f);
        }
    }

    public int GetMichelinStarCount()
    {
        return michelinStarCount;
    }
}

