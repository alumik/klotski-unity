using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadNextSceneDelay(float delay)
    {
        Invoke(nameof(LoadNextScene), delay);
    }

    public void LoadPreviousSceneDelay(float delay)
    {
        Invoke(nameof(LoadPreviousScene), delay);
    }

    public void LoadStartSceneDelay(float delay)
    {
        Invoke(nameof(LoadStartScene), delay);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadStartScene()
    {
        Store.LastSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(0);
    }

    public void LoadNextScene()
    {
        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        Store.LastSceneIndex = currentSceneIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }

    public void LoadPreviousScene()
    {
        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        Store.LastSceneIndex = currentSceneIndex;
        SceneManager.LoadScene(currentSceneIndex - 1);
    }
}