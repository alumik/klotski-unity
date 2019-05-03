using System.Collections;
using Common;
using Scene_0;
using UnityEngine;

namespace Scene_2
{
    public class StageAnimator : MonoBehaviour
    {
        [SerializeField] private Animator[] animators;

        private void Awake()
        {
            StartCoroutine(AudioLowPassIncrease());
        }

        public void Back()
        {
            StartCoroutine(AudioLowPassDecrease());
            PlayHideAnimation();
            GetComponent<SceneLoader>().LoadPreviousSceneDelay((float) 0.75);
        }

        public void GameWon()
        {
            StartCoroutine(AudioLowPassDecrease());
            PlayHideAnimation();
            GetComponent<SceneLoader>().LoadNextSceneDelay((float) 0.75);
        }

        private void PlayHideAnimation()
        {
            foreach (var animator in animators)
            {
                animator.Play(animator.gameObject.name + " Hide");
            }
        }

        private static IEnumerator AudioLowPassIncrease()
        {
            var filter = BackgroundMusic.Instance.GetComponent<AudioLowPassFilter>();
            filter.enabled = true;
            for (float f = 22000; f >= 1000; f -= 500)
            {
                filter.cutoffFrequency = f;
                yield return new WaitForSeconds((float) 0.01);
            }
        }

        private static IEnumerator AudioLowPassDecrease()
        {
            var filter = BackgroundMusic.Instance.GetComponent<AudioLowPassFilter>();
            for (float f = 1000; f <= 22000; f += 500)
            {
                filter.cutoffFrequency = f;
                yield return new WaitForSeconds((float) 0.01);
            }

            filter.enabled = false;
        }
    }
}