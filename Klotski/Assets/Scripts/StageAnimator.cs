using UnityEngine;

public class StageAnimator : MonoBehaviour
{
    public void Back()
    {
        PlayHideAnimation();
        FindObjectOfType<SceneLoader>().LoadPreviousSceneDelay((float) 0.75);
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
}