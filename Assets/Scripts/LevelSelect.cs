using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{
    public LevelObject[] levelObjects;
    public static int currentLevel;
    public static int unlockedLevels;
    public void GoBack()
    {
        SceneManager.LoadScene("StartScreen");
    }

    public void LoadLevel(int levelNumber)
    {
        currentLevel = levelNumber;
        SceneManager.LoadScene("Level" + (levelNumber + 1));
    }

    private void Start()
    {
        unlockedLevels = PlayerPrefs.GetInt("UnlockedLevels", 0);
        for (int i = 0; i < levelObjects.Length; i++)
        {
            if (unlockedLevels >= i)
            {
                levelObjects[i].levelButton.interactable = true;
                int stars = PlayerPrefs.GetInt("stars" + i.ToString(), 0);
                for (int j = 0; j < stars; j++)
                {
                    levelObjects[i].stars[j].color = Color.white;
                }
            }
        }
    }
}
