using System;
using System.Collections.Generic;
using Common;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Scene_1
{
    public class StageScrollView : MonoBehaviour
    {
        [Header("Stage Configs")]
        [SerializeField]private StageConfig[] tutorialStages;
        [SerializeField] private StageConfig[] easyStages;
        [SerializeField] private StageConfig[] hardStages;

        [Header("Prefabs")]
        [SerializeField] private GameObject stageItem;
        [SerializeField] private GameObject selectIndicator;

        [Header("Parameters")]
        [SerializeField] private float startingPosition;
        [SerializeField] private int difficultyNumber;
        [SerializeField] private string[] difficultyString;

        [Header("Interface")]
        [SerializeField] private GameObject scrollContent;
        [SerializeField] private StageSelectorAnimator animator;
        [SerializeField] private GameObject playButton;
        [SerializeField] private GameObject steps;
        [SerializeField] private GameObject time;
        [SerializeField] private GameObject unfinished;
        [SerializeField] private Text changeDifficultyButton;

        private GameObject mCurrentSelectIndicator;
        private StageConfig[] mStageConfigs;
        private List<GameObject> mStageItems;
        private int mDifficulty;

        private void Start()
        {
            mStageConfigs = tutorialStages;
            if (Store.LastSceneIndex != Store.SceneMainMenu)
            {
                RestoreDifficulty();
            }
            GenerateList();
            RestoreScrollPosition();
        }

        private void OnDestroy()
        {
            Store.LastScrollPosition = scrollContent.GetComponent<RectTransform>().localPosition.y;
            Store.Difficulty = mDifficulty;
        }

        public void SwitchDifficulty()
        {
            mDifficulty++;
            if (mDifficulty >= difficultyNumber)
            {
                mDifficulty = 0;
            }
            SetDifficulty(mDifficulty);
            DisposeList();
            GenerateList();
        }
        
        public void PlayRandom()
        {
            animator.LoadStage(mStageConfigs[new System.Random().Next(mStageConfigs.Length)]);
        }

        private void DisposeList()
        {
            if (mCurrentSelectIndicator != null)
            {
                Destroy(mCurrentSelectIndicator);
            }
            foreach (var stageItemObject in mStageItems)
            {
                Destroy(stageItemObject);
            }
            playButton.GetComponentInChildren<TextMeshProUGUI>().text = "\uf074";
            unfinished.SetActive(true);
            steps.SetActive(false);
            time.SetActive(false);
            unfinished.GetComponent<Text>().text = "随机游戏";
            playButton.GetComponent<Button>().onClick.RemoveAllListeners();
            playButton.GetComponent<Button>().onClick.AddListener(PlayRandom);
            var pos = scrollContent.GetComponent<RectTransform>().localPosition;
            scrollContent.GetComponent<RectTransform>().localPosition =
                new Vector3(pos.x, 0, pos.z);
        }
        
        private void GenerateList()
        {
            mStageItems = new List<GameObject>();
            var itemHeight = stageItem.GetComponent<RectTransform>().rect.height;
            for (var i = 0; i < mStageConfigs.Length; i++)
            {
                var stageConfig = mStageConfigs[i];
                var stageItemObject = Instantiate(stageItem, scrollContent.transform);
                stageItemObject.name = stageConfig.GetStageName();
                stageItemObject.GetComponent<RectTransform>().anchoredPosition = new Vector3(
                    (float) (i % 2 == 0 ? -232.52 : 235),
                    // ReSharper disable once PossibleLossOfFraction
                    startingPosition - i / 2 * itemHeight,
                    0);
                var finished = Store.Db.GetFinished(stageConfig.GetStageId());
                var stageName = stageItemObject.name;
                if (finished)
                {
                    stageName = "■ " + stageName;
                }
                else
                {
                    stageName = "□ " + stageName;
                }

                stageItemObject.GetComponentInChildren<Text>().text = stageName;
                stageItemObject.GetComponent<Button>().onClick.AddListener(() =>
                {
                    StageItemSelected(stageItemObject, stageConfig);
                });
                mStageItems.Add(stageItemObject);
            }

            scrollContent.GetComponent<RectTransform>().sizeDelta =
                new Vector2(0, Mathf.Ceil(mStageConfigs.Length / 2.0f) * itemHeight);
        }
        
        private void SetDifficulty(int difficulty)
        {
            changeDifficultyButton.text = difficultyString[mDifficulty];
            switch (difficulty)
            {
                case 0:
                    mStageConfigs = tutorialStages;
                    break;
                case 1:
                    mStageConfigs = easyStages;
                    break;
                case 2:
                    mStageConfigs = hardStages;
                    break;
                default:
                    mStageConfigs = tutorialStages;
                    break;
            }
        }

        private void RestoreDifficulty()
        {
            mDifficulty = Store.Difficulty;
            SetDifficulty(mDifficulty);
        }

        private void RestoreScrollPosition()
        {
            if (Store.LastSceneIndex != Store.SceneMainMenu)
            {
                var pos = scrollContent.GetComponent<RectTransform>().localPosition;
                scrollContent.GetComponent<RectTransform>().localPosition =
                    new Vector3(pos.x, Store.LastScrollPosition, pos.z);
            }
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
                unfinished.SetActive(true);
                unfinished.GetComponent<Text>().text = "未完成";
                steps.SetActive(false);
                time.SetActive(false);
            }
        }
    }
}