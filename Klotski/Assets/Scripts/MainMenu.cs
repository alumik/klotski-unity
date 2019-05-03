using System;
using System.Diagnostics;
using FantomLib;
using TMPro;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI bgmButton;

    private void Start()
    {
        ChangeBgmButton();
    }

    private void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            FindObjectOfType<YesNoDialogController>().Show();
        }
    }

    public void ToggleBackgroundMusic()
    {
        if (BackgroundMusic.Instance.GetComponent<AudioSource>().isPlaying)
        {
            BackgroundMusic.Instance.GetComponent<AudioSource>().Pause();
        }
        else
        {
            BackgroundMusic.Instance.GetComponent<AudioSource>().UnPause();
        }

        ChangeBgmButton();
    }

    private void ChangeBgmButton()
    {
        if (BackgroundMusic.Instance.GetComponent<AudioSource>().isPlaying)
        {
            bgmButton.text = "\uf026";
        }
        else
        {
            bgmButton.text = "\uf6a9";
        }
    }

    private void OnApplicationQuit()
    {
        Process.GetCurrentProcess().Kill();
        ProcessThreadCollection pt = Process.GetCurrentProcess().Threads;
        foreach (ProcessThread p in pt)
        {
            p.Dispose();
        }
    }
}