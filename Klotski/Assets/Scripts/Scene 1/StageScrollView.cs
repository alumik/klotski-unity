using System;
using Common;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Scene_1
{
    public class StageScrollView : MonoBehaviour
    {
        [SerializeField] private StageConfig[] stageConfigs;
        [SerializeField] private GameObject scrollContent;
        [SerializeField] private GameObject stageItem;
        [SerializeField] private GameObject selectIndicator;
        [SerializeField] private float startingPosition;
        [SerializeField] private StageSelectorAnimator animator;
        [SerializeField] private GameObject playButton;
        [SerializeField] private GameObject steps;
        [SerializeField] private GameObject time;
        [SerializeField] private GameObject unfinished;

        private GameObject mCurrentSelectIndicator;

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
                    new Vector3((float) (i % 2 == 0 ? -232.52 : 235), startingPosition - i / 2 * itemHeight, 0);
                var stageConfig = stageConfigs[i];
                stageItemObject.GetComponent<Button>().onClick.AddListener(() =>
                {
                    StageItemSelected(stageItemObject, stageConfig);
                });
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

        public void RandomGame()
        {
            animator.LoadStage(stageConfigs[new System.Random().Next(stageConfigs.Length)]);
        }

        private void StageItemSelected(GameObject stageItemObject, StageConfig stageConfig)
        {
            if (mCurrentSelectIndicator != null)
            {
                Destroy(mCurrentSelectIndicator);
            }

            mCurrentSelectIndicator = Instantiate(selectIndicator, stageItemObject.transform);
            mCurrentSelectIndicator.GetComponent<RectTransform>().localPosition = new Vector3(0, -56, 0);

            playButton.GetComponentInChildren<TextMeshProUGUI>().text = "\uf04b";
            playButton.GetComponent<Button>().onClick.RemoveAllListeners();
            playButton.GetComponent<Button>().onClick.AddListener(() => { animator.LoadStage(stageConfig); });

            var result = Store.Db.GetResult(stageConfig.GetStageId());
            if (result.Finished)
            {
                unfinished.SetActive(false);
                steps.SetActive(true);
                time.SetActive(true);
                steps.transform.GetChild(0).GetComponent<Text>().text = result.BestSteps.ToString();
                var timeSpan = TimeSpan.FromSeconds(result.BestTime);
                var timeText = $"{timeSpan.Hours:D2}:{timeSpan.Minutes:D2}:{timeSpan.Seconds:D2}";
                time.transform.GetChild(0).GetComponent<Text>().text = timeText;
            }
            else
            {
                unfinished.GetComponent<Text>().text = "未完成";
                steps.SetActive(false);
                time.SetActive(false);
            }
        }
    }
}