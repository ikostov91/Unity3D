using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void StartGame()
    {
        int sceneIndex = 1;
        SceneManager.LoadScene(sceneIndex);
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
    }

    public void ContinueGame()
    {
        Time.timeScale = 1f;
    }

    public void PlayAgain()
    {
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(sceneIndex);
        Time.timeScale = 1f;
    }

    public void QuitLevel()
    {
        int sceneIndex = 0;
        SceneManager.LoadScene(sceneIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }    
}
