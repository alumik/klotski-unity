using UnityEngine;

public class Stage : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            FindObjectOfType<StageAnimator>().Back();
        }
    }
}