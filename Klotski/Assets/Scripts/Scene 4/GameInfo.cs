using Common;
using UnityEngine;
using UnityEngine.UI;

namespace Scene_4
{
    public class GameInfo : MonoBehaviour
    {
        [SerializeField] private string versionString;
        [SerializeField] private Text versionStringText;

        private void Start()
        {
            if (Camera.main != null)
            {
                Camera.main.backgroundColor = Store.CurrentColor;
            }
            versionStringText.text = versionString;
        }
    }
}