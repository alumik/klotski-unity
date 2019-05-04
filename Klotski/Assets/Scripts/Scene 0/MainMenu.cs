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
            Store.CurrentColorIndex = Store.Db.GetConfig(Store.ConfigColor);
            ApplyColor();
            SetBackgroundMusic();
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
                Store.Db.SetConfig(Store.ConfigBgm, 0);
            }
            else
            {
                audioSource.UnPause();
                Store.Db.SetConfig(Store.ConfigBgm, 1);
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

            ApplyColor();
            Store.Db.SetConfig(Store.ConfigColor, Store.CurrentColorIndex);
        }

        private static void ApplyColor()
        {
            Store.CurrentColor = new Color32(Store.Colors[Store.CurrentColorIndex, 0],
                Store.Colors[Store.CurrentColorIndex, 1], Store.Colors[Store.CurrentColorIndex, 2], 0xFF);
            if (Camera.main != null)
            {
                Camera.main.backgroundColor = Store.CurrentColor;
            }
        }

        private void ChangeBgmButton()
        {
            bgmButton.text = BackgroundMusic.Instance.GetComponent<AudioSource>().isPlaying ? "\uf026" : "\uf6a9";
        }

        private void SetBackgroundMusic()
        {
            if (Store.Db.GetConfig(Store.ConfigBgm) == 0)
            {
                BackgroundMusic.Instance.GetComponent<AudioSource>().Pause();
            }
        }
    }
}