using UnityEngine;

public class GoalTrigger : MonoBehaviour
{
    [SerializeField] private WinScreen winScreen;

    // OnTriggerEnter is called when the Collider other enters the trigger
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            winScreen.ShowWinScreen();
        }
    }
}
