using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadNextScene()
    {
        Invoke(nameof(_LoadNextScene), (float) 0.5);
    }

    public void LoadStartScene()
    {
        Invoke(nameof(_LoadStartScene), (float) 0.5);
    }

    public void QuitGame()
    {
        Invoke(nameof(_QuitGame), (float) 0.5);
    }

    private void _QuitGame()
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

    private void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            if (SceneManager.GetActiveScene().buildIndex == 0)
            {
                QuitGame();
            }
            else
            {
                LoadStartScene();
            }
        }
    }
}