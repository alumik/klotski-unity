using UnityEngine;
using UnityEngine.UI;

public class StageSelector : MonoBehaviour
{
    [SerializeField] private StageConfig[] stageConfigs;
    [SerializeField] private GameObject scrollContent;
    [SerializeField] private GameObject stageItem;
    [SerializeField] private float startingPosition;

    void Start()
    {
        var itemHeight = stageItem.GetComponent<RectTransform>().rect.height;
        for (var i = 0; i < stageConfigs.Length; i++)
        {
            var stageItemObject = Instantiate(stageItem, scrollContent.transform);
            stageItemObject.name = stageConfigs[i].GetStageName();
            stageItemObject.GetComponentInChildren<Text>().text = stageConfigs[i].GetStageName();
            stageItemObject.GetComponent<RectTransform>().anchoredPosition =
                new Vector3(0, startingPosition - i * itemHeight, 0);
            var stageConfig = stageConfigs[i];
            stageItemObject.GetComponent<Button>().onClick.AddListener(() => { LoadStage(stageConfig); });
        }

        scrollContent.GetComponent<RectTransform>().sizeDelta = new Vector2(0, stageConfigs.Length * itemHeight);
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