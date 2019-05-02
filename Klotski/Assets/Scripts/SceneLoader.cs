using FantomLib;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadNextScene()
    {
        Invoke(nameof(_LoadNextScene), (float) 0.5);
    }
    
    public void LoadPreviousScene()
    {
        Invoke(nameof(_LoadPreviousScene), (float) 0.5);
    }

    public void LoadStartScene()
    {
        Invoke(nameof(_LoadStartScene), (float) 0.5);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private void _LoadStartScene()
    {
        SceneManager.LoadScene(0);
    }

    public void _LoadNextScene()
    {
        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }
    
    public void _LoadPreviousScene()
    {
        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex - 1);
    }
}