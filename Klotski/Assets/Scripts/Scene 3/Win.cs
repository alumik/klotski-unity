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
        [SerializeField] private Text stageName;

        private void Start()
        {
            if (Camera.main != null)
            {
                Camera.main.backgroundColor = Store.CurrentColor;
            }

            Store.Db.SaveResult(Store.NextStageConfig.GetStageId(), Store.Steps, Store.Time);
            var timeSpan = TimeSpan.FromSeconds(Store.Time);
            var timeText = $"{timeSpan.Hours:D2}:{timeSpan.Minutes:D2}:{timeSpan.Seconds:D2}";
            time.text = "用时 " + timeText;
            stepCounter.text = "总步数 " + Store.Steps;
            stageName.text = Store.NextStageConfig.GetStageName();
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