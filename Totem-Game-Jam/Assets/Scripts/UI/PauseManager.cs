using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class PauseManager : MonoBehaviour
{
    // Script for managing the in-game pause screen
    [SerializeField] GameObject pauseScreen;
    [SerializeField] GameObject helpScreen;
    [SerializeField] GameObject HUD;
    private SFXPlayer sfxPlayer;
    private bool paused;

    private void Start()
    {
        sfxPlayer = FindFirstObjectByType<SFXPlayer>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!paused)
                Pause();
            else
                Unpause();
        }
    }

    public void Pause()
    {
        sfxPlayer.ClickButtonSound();
        pauseScreen.SetActive(true);
        HUD.SetActive(false);
        paused = true;
        Time.timeScale = 0;
    }

    public void Unpause()
    {
        sfxPlayer.ClickButtonSound();
        helpScreen.SetActive(false);
        pauseScreen.SetActive(false);
        HUD.SetActive(true);
        paused = false;
        Time.timeScale = 1;
    }

    public void BackToMenu()
    {
        sfxPlayer.ClickButtonSound();
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    public void HelpScreen()
    {
        sfxPlayer.ClickButtonSound();
        helpScreen.SetActive(true);
    }

    public void LeaveHelpScreen()
    {
        sfxPlayer.ClickButtonSound();
        helpScreen.SetActive(false);
    }
}
