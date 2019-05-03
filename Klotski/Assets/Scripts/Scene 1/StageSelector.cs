using Common;
using Scene_0;
using TMPro;
using UnityEngine;

namespace Scene_1
{
    public class StageSelector : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI bgmButton;

        private void Start()
        {
            if (Camera.main != null)
            {
                Camera.main.backgroundColor = Store.CurrentColor;
            }
            ChangeBgmButton();
        }

        private void Update()
        {
            if (Input.GetKeyDown("escape"))
            {
                GetComponent<StageSelectorAnimator>().Back();
            }
        }

        private void ChangeBgmButton()
        {
            if (BackgroundMusic.Instance != null)
            {
                bgmButton.text = BackgroundMusic.Instance.GetComponent<AudioSource>().isPlaying ? "\uf026" : "\uf6a9";
            }
        }
    }
}