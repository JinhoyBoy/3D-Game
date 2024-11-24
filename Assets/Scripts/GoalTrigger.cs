using UnityEngine;

public class GoalTrigger : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField] private WinScreen winScreen;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // OnTriggerEnter is called when the Collider other enters the trigger
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            winScreen.ShowWinScreen();
            audioSource.Play();
        }
    }
}
