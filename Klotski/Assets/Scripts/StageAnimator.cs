using System.Collections;
using UnityEngine;

public class StageAnimator : MonoBehaviour
{
    public void Back()
    {
        StartCoroutine(AudioLowPassDecrease());
        PlayHideAnimation();
        gameObject.GetComponent<SceneLoader>().LoadPreviousSceneDelay((float) 0.75);
    }

    public void GameWon()
    {
        StartCoroutine(AudioLowPassDecrease());
        PlayHideAnimation();
        gameObject.GetComponent<SceneLoader>().LoadNextSceneDelay((float) 0.75);
    }

    private static void PlayHideAnimation()
    {
        var anim = GameObject.Find("Back Button").GetComponent<Animator>();
        anim.Play("Back Button Hide");
        anim = GameObject.Find("Reset Button").GetComponent<Animator>();
        anim.Play("Test Stage Button Hide");
        anim = GameObject.Find("Play Area").GetComponent<Animator>();
        anim.Play("Play Area Hide");
        anim = GameObject.Find("Stage Name").GetComponent<Animator>();
        anim.Play("Stage Name Hide");
        anim = GameObject.Find("Background Panel").GetComponent<Animator>();
        anim.Play("Background Panel Hide");
        anim = GameObject.Find("Statistics").GetComponent<Animator>();
        anim.Play("Statistics Hide");
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