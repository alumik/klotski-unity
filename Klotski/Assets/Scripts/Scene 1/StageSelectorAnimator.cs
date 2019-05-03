using Common;
using UnityEngine;

namespace Scene_1
{
    public class StageSelectorAnimator : MonoBehaviour
    {
        [SerializeField] private Animator[] animators;

        private void Start()
        {
            if (Store.LastSceneIndex == Store.SceneMainMenu)
            {
                GameObject.Find("Button Panel To Hide").GetComponent<Animator>().Play("Button Panel To Hide Hide");
            }
        }

        public void Back()
        {
            PlayHideAnimation();
            GetComponent<SceneLoader>().LoadPreviousSceneDelay((float) 0.5);
        }

        public void LoadStage(StageConfig stageConfig)
        {
            Store.NextStageConfig = stageConfig;
            PlayHideAnimation();
            GetComponent<SceneLoader>().LoadNextSceneDelay((float) 0.5);
        }

        public void GameInfo()
        {
            PlayHideAnimation();
            GetComponent<SceneLoader>().LoadSceneDelay(Store.SceneGameInfo, (float) 0.5);
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