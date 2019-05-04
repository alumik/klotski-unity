using Common;
using UnityEngine;
using UnityEngine.UI;

namespace Scene_2
{
    public class Stage : MonoBehaviour
    {
        [SerializeField] private PlayArea playArea;
        [SerializeField] private Text stageName;
        [SerializeField] private GameObject exitBar;

        private void Start()
        {
            if (Camera.main != null)
            {
                Camera.main.backgroundColor = Store.CurrentColor;
            }

            exitBar.GetComponent<SpriteRenderer>().color = Store.CurrentColor;
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