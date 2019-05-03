using Common;
using UnityEngine;

namespace Scene_4
{
    public class GameInfoAnimator : MonoBehaviour
    {
        [SerializeField] private Animator[] animators;

        public void Back()
        {
            PlayHideAnimation();
            GetComponent<SceneLoader>().LoadSceneDelay(Store.SceneLevelSelector, (float) 0.5);
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