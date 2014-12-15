﻿using UnityEngine;
using System.Collections;

public class Game
{
    public void loadGameScene(string sceneName)
    {
        Application.LoadLevel(sceneName);
    }

    public void startNewGame()
    {
        Application.LoadLevel("GameScene");
    }

    public void returnToMainMenu()
    {
        Application.LoadLevel("MainMenuScene");
    }

    public void restartLevel()
    {
        Application.LoadLevel(Application.loadedLevelName);
    }

    public void quitGame()
    {
        Application.Quit();
    }

    public string getUserProfile()
    {
        string profileName = "NoProfile";
        return profileName;
    }

    public void createNewProfile()
    {

    }

    public void loadProfile()
    {

    }

    
}
