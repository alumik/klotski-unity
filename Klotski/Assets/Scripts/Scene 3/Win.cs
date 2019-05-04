using System;
using Common;
using UnityEngine;
using UnityEngine.UI;

namespace Scene_3
{
    public class Win : MonoBehaviour
    {
        [SerializeField] private Text stepCounter;
        [SerializeField] private Text time;
        [SerializeField] private Text bestStepCounter;
        [SerializeField] private Text bestTime;
        [SerializeField] private Text stageName;

        private void Start()
        {
            if (Camera.main != null)
            {
                Camera.main.backgroundColor = Store.CurrentColor;
            }

            var stageConfig = Store.NextStageConfig;

            Store.Db.SaveResult(stageConfig.GetStageId(), Store.Steps, Store.Time);
            var timeSpan = TimeSpan.FromSeconds(Store.Time);
            var timeText = $"{timeSpan.Hours:D2}:{timeSpan.Minutes:D2}:{timeSpan.Seconds:D2}";
            time.text = "用时 " + timeText;
            stepCounter.text = "总步数 " + Store.Steps;
            stageName.text = stageConfig.GetStageName();
            
            var result = Store.Db.GetResult(stageConfig.GetStageId());
            bestStepCounter.text = result.BestSteps.ToString();
            timeSpan = TimeSpan.FromSeconds(result.BestTime);
            timeText = $"{timeSpan.Hours:D2}:{timeSpan.Minutes:D2}:{timeSpan.Seconds:D2}";
            bestTime.text = timeText;
        }

        private void Update()
        {
            if (Input.GetKeyDown("escape"))
            {
                GetComponent<WinAnimator>().Home();
            }
        }
    }
}