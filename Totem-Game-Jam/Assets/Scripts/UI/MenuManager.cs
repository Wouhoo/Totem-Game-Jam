using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    // Script for managing the main menu.
    [SerializeField] GameObject helpScreen;
    [SerializeField] GameObject levelSelectScreen;
    private SFXPlayer sfxPlayer;

    void Start()
    {
        sfxPlayer = FindFirstObjectByType<SFXPlayer>();
    }

    public void StartGame()
    {
        sfxPlayer.ClickButtonSound();
        levelSelectScreen.SetActive(true);
    }

    public void LeaveLevelSelect()
    {
        sfxPlayer.ClickButtonSound();
        levelSelectScreen.SetActive(false);
    }

    public void QuitGame()
    {
        sfxPlayer.ClickButtonSound();
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

    public void HelpScreen()
    {
        sfxPlayer.ClickButtonSound();
        Time.timeScale = 0;
        helpScreen.SetActive(true);
    }

    public void LeaveHelpScreen()
    {
        sfxPlayer.ClickButtonSound();
        Time.timeScale = 1;
        helpScreen.SetActive(false);
    }

    public void LoadLevel(int level)
    {
        sfxPlayer.ClickButtonSound();
        SceneManager.LoadScene(level); // Make sure all levels are added to build settings in the right order
    }
}
