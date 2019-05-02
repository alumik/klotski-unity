using FantomLib;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            FindObjectOfType<YesNoDialogController>().Show();
        }
    }
}