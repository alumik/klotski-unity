using Common;
using UnityEngine;

namespace Scene_1
{
    public class StageSelector : MonoBehaviour
    {
        private void Start()
        {
            if (Camera.main != null)
            {
                Camera.main.backgroundColor = Store.CurrentColor;
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown("escape"))
            {
                GetComponent<StageSelectorAnimator>().Back();
            }
        }
    }
}