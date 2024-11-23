using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScreen : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("LevelSelection");  
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
