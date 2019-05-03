using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StageSelector : MonoBehaviour
{
    [SerializeField] private StageConfig[] stageConfigs;
    [SerializeField] private GameObject scrollContent;
    [SerializeField] private GameObject stageItem;
    [SerializeField] private float startingPosition;
    [SerializeField] private TextMeshProUGUI bgmButton;

    void Start()
    {
        var itemHeight = stageItem.GetComponent<RectTransform>().rect.height;
        for (var i = 0; i < stageConfigs.Length; i++)
        {
            var stageItemObject = Instantiate(stageItem, scrollContent.transform);
            stageItemObject.name = stageConfigs[i].GetStageName();
            stageItemObject.GetComponentInChildren<Text>().text = stageConfigs[i].GetStageName();
            stageItemObject.GetComponent<RectTransform>().anchoredPosition =
                new Vector3((float) (i % 2 == 0 ? -232.52 : 232.52), startingPosition - (i / 2) * itemHeight, 0);
            var stageConfig = stageConfigs[i];
            stageItemObject.GetComponent<Button>().onClick.AddListener(() =>
            {
                FindObjectOfType<StageSelectorAnimator>().LoadStage(stageConfig);
            });
        }

        scrollContent.GetComponent<RectTransform>().sizeDelta = new Vector2(0, Mathf.Ceil(stageConfigs.Length / 2.0f) * itemHeight);
        
        if (BackgroundMusic.Instance && BackgroundMusic.Instance.GetComponent<AudioSource>().isPlaying)
        {
            bgmButton.text = "\uf026";
        }
        else
        {
            bgmButton.text = "\uf6a9";
        }

        if (Store.LastSceneIndex != 0)
        {
            var pos = scrollContent.GetComponent<RectTransform>().localPosition;
            scrollContent.GetComponent<RectTransform>().localPosition = new Vector3(pos.x, Store.ScrollPosition, pos.z);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            FindObjectOfType<StageSelectorAnimator>().Back();
        }
    }

    private void OnDestroy()
    {
        Store.ScrollPosition = scrollContent.GetComponent<RectTransform>().localPosition.y;
    }
}