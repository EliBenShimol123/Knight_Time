using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class OpeningManager : MonoBehaviour
{

    [SerializeField] private Button startButton;
    [SerializeField] private Button creditsButton;
    [SerializeField] private Button exitButton;
    [SerializeField] private Button backButton;
    [SerializeField] private TMP_Text credits_text;
    public void startGame()
    {
        LevelLoader.instance.loadStartingScene();
    }

    public void showCredits()
    {
        startButton.gameObject.SetActive(false);
        creditsButton.gameObject.SetActive(false);
        exitButton.gameObject.SetActive(false);
        backButton.gameObject.SetActive(true);
        credits_text.gameObject.SetActive(true);
    }

    public void backToMain()
    {
        startButton.gameObject.SetActive(true);
        creditsButton.gameObject.SetActive(true);
        exitButton.gameObject.SetActive(true);
        backButton.gameObject.SetActive(false);
        credits_text.gameObject.SetActive(false);
    }

    public void exitGame()
    {
        Application.Quit();
    }
}
