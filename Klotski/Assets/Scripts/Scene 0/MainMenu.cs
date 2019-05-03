using Common;
using FantomLib;
using TMPro;
using UnityEngine;

namespace Scene_0
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI bgmButton;
        [SerializeField] private YesNoDialogController exitDialog;

        private void Start()
        {
            ApplyColor();
            ChangeBgmButton();
        }

        private void Update()
        {
            if (Input.GetKeyDown("escape"))
            {
                exitDialog.Show();
            }
        }

        public void ToggleBackgroundMusic()
        {
            var audioSource = BackgroundMusic.Instance.GetComponent<AudioSource>();
            if (audioSource.isPlaying)
            {
                audioSource.Pause();
            }
            else
            {
                audioSource.UnPause();
            }

            ChangeBgmButton();
        }

        public void ChangeColor()
        {
            Store.CurrentColorIndex++;
            if (Store.CurrentColorIndex >= Store.Colors.Length / 3)
            {
                Store.CurrentColorIndex = 0;
            }

            Store.CurrentColor = new Color32(Store.Colors[Store.CurrentColorIndex, 0],
                Store.Colors[Store.CurrentColorIndex, 1], Store.Colors[Store.CurrentColorIndex, 2], 0xFF);
            ApplyColor();
        }

        private static void ApplyColor()
        {
            if (Camera.main != null)
            {
                Camera.main.backgroundColor = Store.CurrentColor;
            }
        }

        private void ChangeBgmButton()
        {
            bgmButton.text = BackgroundMusic.Instance.GetComponent<AudioSource>().isPlaying ? "\uf026" : "\uf6a9";
        }
    }
}