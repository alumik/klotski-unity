using UnityEngine;

public class TestStage : MonoBehaviour
{
    public void LoadStage(StageConfig stageConfig)
    {
        Store.NextStageConfig = stageConfig;
        var anim = GameObject.Find("Back Button").GetComponent<Animator>();
        anim.Play("Back Button Hide");
        anim = GameObject.Find("Test Stage Button").GetComponent<Animator>();
        anim.Play("Test Stage Button Hide");
        anim = GameObject.Find("Scroll View").GetComponent<Animator>();
        anim.Play("Scroll View Hide");
        FindObjectOfType<SceneLoader>().LoadNextSceneDelay((float) 0.5);
    }
}