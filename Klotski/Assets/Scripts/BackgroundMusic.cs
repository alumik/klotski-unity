using UnityEngine;

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