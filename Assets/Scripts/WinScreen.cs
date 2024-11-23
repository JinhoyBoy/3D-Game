using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScreen : MonoBehaviour
{
    [SerializeField] private GameObject winScreen;
    [SerializeField] private GameObject noStar;
    [SerializeField] private GameObject oneStar;
    [SerializeField] private GameObject twoStars;
    [SerializeField] private GameObject threeStars;

    [SerializeField] private Stars starsComponent;

    private void Start()
    {

    }

    public void ShowWinScreen()
    {
        if (winScreen == null)
        {
            Debug.LogError("WinScreen GameObject is not assigned in the Inspector!");
            return;
        }

        if (starsComponent == null)
        {
            Debug.LogError("Stars component is missing. Cannot retrieve Michelin Star Count.");
            return;
        }

        int michelinStarCount = starsComponent.GetMichelinStarCount();
        Debug.Log("Player wins. Michelin Star Count: " + michelinStarCount);

        // Activate the win screen
        winScreen.SetActive(true);
        Time.timeScale = 0;

        // Reset all stars
        ResetStars();

        // Activate the correct star based on the count
        switch (michelinStarCount)
        {
            case 0:
                noStar?.SetActive(true);
                break;
            case 1:
                oneStar?.SetActive(true);
                break;
            case 2:
                twoStars?.SetActive(true);
                break;
            case 3:
                threeStars?.SetActive(true);
                break;
            default:
                Debug.LogWarning("Invalid Michelin Star Count: " + michelinStarCount);
                break;
        }
    }

    public void RestartGame()
    {
        // Reset time scale in case it's paused
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoToLevelSelection()
    {
        // Reset time scale and load level selection scene
        Time.timeScale = 1;
        if (LevelSelect.currentLevel == LevelSelect.unlockedLevels)
        {
            LevelSelect.unlockedLevels++;
            PlayerPrefs.SetInt("UnlockedLevels", LevelSelect.unlockedLevels);
        }
        int michelinStarCount = starsComponent.GetMichelinStarCount();
        PlayerPrefs.SetInt("stars" + LevelSelect.currentLevel.ToString(), michelinStarCount);
        SceneManager.LoadScene("LevelSelection");
    }

    private void ResetStars()
    {
        // Deactivate all star objects
        noStar?.SetActive(false);
        oneStar?.SetActive(false);
        twoStars?.SetActive(false);
        threeStars?.SetActive(false);
    }
}
