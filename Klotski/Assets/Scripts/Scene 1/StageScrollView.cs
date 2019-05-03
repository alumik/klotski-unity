using Common;
using UnityEngine;
using UnityEngine.UI;

namespace Scene_1
{
    public class StageScrollView : MonoBehaviour
    {
        [SerializeField] private StageConfig[] stageConfigs;
        [SerializeField] private GameObject scrollContent;
        [SerializeField] private GameObject stageItem;
        [SerializeField] private float startingPosition;
        [SerializeField] private StageSelectorAnimator animator;

        private void Start()
        {
            var itemHeight = stageItem.GetComponent<RectTransform>().rect.height;
            for (var i = 0; i < stageConfigs.Length; i++)
            {
                var stageItemObject = Instantiate(stageItem, scrollContent.transform);
                stageItemObject.name = stageConfigs[i].GetStageName();
                stageItemObject.GetComponentInChildren<Text>().text = stageConfigs[i].GetStageName();
                stageItemObject.GetComponent<RectTransform>().anchoredPosition =
                    // ReSharper disable once PossibleLossOfFraction
                    new Vector3((float) (i % 2 == 0 ? -232.52 : 232.52), startingPosition - i / 2 * itemHeight, 0);
                var stageConfig = stageConfigs[i];
                stageItemObject.GetComponent<Button>().onClick.AddListener(() => { animator.LoadStage(stageConfig); });
            }

            scrollContent.GetComponent<RectTransform>().sizeDelta =
                new Vector2(0, Mathf.Ceil(stageConfigs.Length / 2.0f) * itemHeight);


            if (Store.LastSceneIndex != Store.SceneMainMenu)
            {
                var pos = scrollContent.GetComponent<RectTransform>().localPosition;
                scrollContent.GetComponent<RectTransform>().localPosition =
                    new Vector3(pos.x, Store.LastScrollPosition, pos.z);
            }
        }

        private void OnDestroy()
        {
            Store.LastScrollPosition = scrollContent.GetComponent<RectTransform>().localPosition.y;
        }
    }
}