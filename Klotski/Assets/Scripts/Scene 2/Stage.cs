using Common;
using UnityEngine;
using UnityEngine.UI;

namespace Scene_2
{
    public class Stage : MonoBehaviour
    {
        [SerializeField] private PlayArea playArea;
        [SerializeField] private Text stageName;

        private void Start()
        {
            stageName.text = Store.NextStageConfig.GetStageName();
        }

        private void Update()
        {
            if (Input.GetKeyDown("escape"))
            {
                GetComponent<StageAnimator>().Back();
            }
        }

        public void ResetGame()
        {
            playArea.ResetGame();
        }
    }
}