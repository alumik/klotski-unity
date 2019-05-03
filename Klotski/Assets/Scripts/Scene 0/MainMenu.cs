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

        private void ChangeBgmButton()
        {
            bgmButton.text = BackgroundMusic.Instance.GetComponent<AudioSource>().isPlaying ? "\uf026" : "\uf6a9";
        }
    }
}