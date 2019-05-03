using UnityEngine;

public class StageSelectorAnimator : MonoBehaviour
{
    private void Start()
    {
        if (Store.LastSceneIndex == 0)
        {
            var anim = GameObject.Find("Button Panel Hide").GetComponent<Animator>();
            anim.Play("Button Panel Hide");
        }
    }
    
    public void Back()
    {
        PlayHideAnimation();
        gameObject.GetComponent<SceneLoader>().LoadPreviousSceneDelay((float) 0.5);
    }
    
    public void LoadStage(StageConfig stageConfig)
    {
        Store.NextStageConfig = stageConfig;
        PlayHideAnimation();
        gameObject.GetComponent<SceneLoader>().LoadNextSceneDelay((float) 0.5);
    }
    
    private static void PlayHideAnimation()
    {
        var anim = GameObject.Find("Back Button").GetComponent<Animator>();
        anim.Play("Back Button Hide");
        anim = GameObject.Find("Test Stage Button").GetComponent<Animator>();
        anim.Play("Test Stage Button Hide");
        anim = GameObject.Find("Stage Selector Title").GetComponent<Animator>();
        anim.Play("Stage Selector Title Hide");
        anim = GameObject.Find("Scroll View").GetComponent<Animator>();
        anim.Play("Scroll View Hide");
    }
}
