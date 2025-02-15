using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class EndScreenManager : MonoBehaviour
{
    // Script for managing the end screen
    private SFXPlayer sfxPlayer;

    void Start()
    {
        sfxPlayer = FindFirstObjectByType<SFXPlayer>();
    }

    public void BackToMenu()
    {
        sfxPlayer.ClickButtonSound();
        SceneManager.LoadScene(0);
    }
}
