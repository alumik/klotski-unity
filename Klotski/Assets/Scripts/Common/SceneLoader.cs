using UnityEngine;
using UnityEngine.SceneManagement;

namespace Common
{
    public class SceneLoader : MonoBehaviour
    {
        public void LoadStartSceneDelay(float delay)
        {
            Invoke(nameof(LoadStartScene), delay);
        }

        public void LoadPreviousSceneDelay(float delay)
        {
            Invoke(nameof(LoadPreviousScene), delay);
        }

        public void LoadNextSceneDelay(float delay)
        {
            Invoke(nameof(LoadNextScene), delay);
        }

        public void LoadStartScene()
        {
            Store.LastSceneIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(0);
        }

        public void LoadPreviousScene()
        {
            var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            Store.LastSceneIndex = currentSceneIndex;
            SceneManager.LoadScene(currentSceneIndex - 1);
        }

        public void LoadNextScene()
        {
            var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            Store.LastSceneIndex = currentSceneIndex;
            SceneManager.LoadScene(currentSceneIndex + 1);
        }

        public void QuitGame()
        {
            Application.Quit();
        }

#if !UNITY_EDITOR
        private void OnApplicationQuit()
        {
            Process.GetCurrentProcess().Kill();
            ProcessThreadCollection pt = Process.GetCurrentProcess().Threads;
            foreach (ProcessThread p in pt)
            {
                p.Dispose();
            }
        }
#endif
    }
}