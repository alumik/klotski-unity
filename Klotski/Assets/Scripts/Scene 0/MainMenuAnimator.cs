using Common;
using UnityEngine;

namespace Scene_0
{
    public class MainMenuAnimator : MonoBehaviour
    {
        [SerializeField] private Animator[] animators;

        public void Next()
        {
            PlayHideAnimation();
            GetComponent<SceneLoader>().LoadNextSceneDelay((float) 0.33);
        }

        public void GameInfo()
        {
            PlayHideAnimation();
            GetComponent<SceneLoader>().LoadSceneDelay(Store.SceneGameInfo, (float) 0.33);
        }

        private void PlayHideAnimation()
        {
            foreach (var animator in animators)
            {
                animator.Play(animator.gameObject.name + " Hide");
            }
        }
    }
}