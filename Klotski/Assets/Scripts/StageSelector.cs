using UnityEngine;
using UnityEngine.UI;

public class StageSelector : MonoBehaviour
{
    [SerializeField] private StageConfig[] allStages;
    [SerializeField] private GameObject scrollContent;
    [SerializeField] private GameObject stageItem;
    [SerializeField] private float startingPosition;

    void Start()
    {
        var itemHeight = stageItem.GetComponent<RectTransform>().rect.height;
        for (var i = 0; i < allStages.Length; i++)
        {
            var stageItemObject = Instantiate(stageItem, scrollContent.transform);
            stageItemObject.name = allStages[i].GetStageName();
            stageItemObject.GetComponentInChildren<Text>().text = allStages[i].GetStageName();
            stageItemObject.GetComponent<RectTransform>().anchoredPosition =
                new Vector3(0, startingPosition - i * itemHeight, 0);
            var stageConfig = allStages[i];
            stageItemObject.GetComponent<Button>().onClick.AddListener(() => { LoadStage(stageConfig); });
        }

        scrollContent.GetComponent<RectTransform>().sizeDelta = new Vector2(0, allStages.Length * itemHeight);
    }

    private void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            FindObjectOfType<SceneLoader>().LoadPreviousScene();
        }
    }

    private void LoadStage(StageConfig stageConfig)
    {
        Store.NextStageConfig = stageConfig;
        FindObjectOfType<SceneLoader>().LoadNextSceneDelay((float) 0.3);
    }
}