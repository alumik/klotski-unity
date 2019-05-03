using UnityEngine;

namespace Scene_0
{
    public class BackgroundMusic : MonoBehaviour
    {
        private static BackgroundMusic mInstance;
        public static BackgroundMusic Instance => mInstance;

        private void Awake()
        {
            if (mInstance != null && mInstance != this)
            {
                Destroy(gameObject);
                return;
            }

            mInstance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
}