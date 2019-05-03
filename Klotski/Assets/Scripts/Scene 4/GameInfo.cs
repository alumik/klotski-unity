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
            versionStringText.text = versionString;
        }
    }
}