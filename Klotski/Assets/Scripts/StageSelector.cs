using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSelector : MonoBehaviour
{
    public void LoadStage(StageConfig stageConfig)
    {
        Store.NextStageConfig = stageConfig;
        FindObjectOfType<SceneLoader>().LoadNextSceneDelay((float) 0.3);
    }
    
    private void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            FindObjectOfType<SceneLoader>().LoadPreviousScene();
        }
    }
}