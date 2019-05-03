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
            time.text = "用时 " + Store.Time;
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