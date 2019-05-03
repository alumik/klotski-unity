using UnityEngine;

public class TestStage : MonoBehaviour
{
    public void LoadStage(StageConfig stageConfig)
    {
        Store.NextStageConfig = stageConfig;
        FindObjectOfType<SceneLoader>().LoadNextSceneDelay((float) 0.3);
    }
}