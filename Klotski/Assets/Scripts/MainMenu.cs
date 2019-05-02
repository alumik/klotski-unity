using FantomLib;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            FindObjectOfType<YesNoDialogController>().Show();
        }
    }

    public void LoadStage()
    {
        Store.NextStageConfig = Resources.Load<StageConfig>("Stages/Stage 1");
        FindObjectOfType<SceneLoader>().LoadNextScene();
    }
}
