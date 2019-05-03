using Common;
using UnityEngine;

namespace Scene_3
{
    public class WinAnimator : MonoBehaviour
    {
        [SerializeField] private Animator[] animators;

        public void Replay()
        {
            PlayHideAnimation();
            GetComponent<SceneLoader>().LoadPreviousSceneDelay((float) 0.5);
        }

        public void Home()
        {
            PlayHideAnimation();
            Store.NextStageConfig = null;
            GetComponent<SceneLoader>().LoadStartSceneDelay((float) 0.5);
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