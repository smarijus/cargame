﻿using UnityEngine;
using System.Collections;

public class UserInterface
{
    private Game game = new Game();
    private int menuButtonWidth = (Screen.width / 10) * 6;
    private int menuButtonHeight = (Screen.height / 20) * 2;
    private int menuBoxLeftPosition = Screen.width / 10;
    private int menuBoxWidth = (Screen.width / 10) * 8;

    // Funkcija atvaizduoja automobilio greitį
    // Parametrai:
    //              carSpeed - automobilio greitis.
    public void showSpeed(float carSpeed)
    {
        GUI.Label(new Rect(Screen.width / 2, Screen.height - 50, 200, 100), "Automobio greitis: " + carSpeed.ToString());
    }

    private Rect getRectanglePositionAndSizeByPercentage(int widthPosition, int heightPosition,  int buttonWidth, int buttonHeight)
    {
        return new Rect();
    }

    public void showInGameControls()
    {
        float horizontalCenter = Screen.width / 2;
        float verticalCenter = Screen.height / 2;
        GUI.Button(new Rect(Screen.width / 50, Screen.height / 25, Screen.width / 10, Screen.height / 10), "M");
        GUI.Button(new Rect(Screen.width / 50, verticalCenter, Screen.width / 10, Screen.height / 5), "H");
        GUI.Button(new Rect(Screen.width / 50, Screen.height - Screen.height / 25 - Screen.height / 5, Screen.width / 10, Screen.height / 5), "B");
        GUI.Button(new Rect((Screen.width / 10 * 9) - Screen.width / 50, (Screen.height / 3 * 2) - Screen.width / 50, Screen.width / 10, Screen.height / 3), "A");
        //GUI.DrawTexture(new Rect(10, 10, 60, 60), aTexture, ScaleMode.ScaleToFit, true, 10.0F);
    }

    public void showCurrentProfile()
    {
        float horizontalCenter = Screen.width / 2;
        float verticalCenter = Screen.height / 2;

        float left = horizontalCenter - 200;
        float top = verticalCenter - 200;
        //float width = 400;
        //float height = 100;

        //GUI.Box(new Rect(left, top, width, height), "Vartotojo paskyra: ");
        GUI.Box(new Rect(menuBoxLeftPosition, Screen.height / 20, menuBoxWidth, (Screen.height / 10) * 2), "Vartotojo paskyra: ");
        if (GUI.Button(new Rect(((Screen.width / 10) * 2), (Screen.height / 20) * 2, menuButtonWidth, menuButtonHeight), game.getUserProfile()))
        {
            showProfileSelection();
        }

    }

    public void showMainMenu()
    {
        float horizontalCenter = Screen.width / 2;
        float verticalCenter = Screen.height / 2;

        float left = horizontalCenter - 200;
        float top = verticalCenter - 100;
        //float width = 400;
        //float height = 250;

        GUI.Box(new Rect(menuBoxLeftPosition, (Screen.height / 20) * 5, menuBoxWidth, (Screen.height / 5) * 3), "Pagrindinis meniu");

        if (GUI.Button(new Rect((Screen.width / 10) * 2, (Screen.height / 20) * 6, menuButtonWidth, menuButtonHeight), "Pradėti žaidimą"))
        {
            game.loadGameScene("GameScene");
        }

        if (GUI.Button(new Rect((Screen.width / 10) * 2, (Screen.height / 20) * 8, menuButtonWidth, menuButtonHeight), "Tęsti išsaugotą žaidimą"))
        {
            //game.quitGame();
        }

        if (GUI.Button(new Rect((Screen.width / 10) * 2, (Screen.height / 20) * 10, menuButtonWidth, menuButtonHeight), "Geriausi rezultatai"))
        {
            //game.quitGame();
        }

        if (GUI.Button(new Rect((Screen.width / 10) * 2, (Screen.height / 20) * 12, menuButtonWidth, menuButtonHeight), "Informacija apie projektą"))
        {
            game.quitGame();
        }

        if (GUI.Button(new Rect((Screen.width / 10) * 2, (Screen.height / 20) * 14, menuButtonWidth, menuButtonHeight), "Išjungti žaidimą"))
        {
            game.quitGame();
        }
    }

    public void showProfileSelection()
    {
        float horizontalCenter = Screen.width / 2;
        float verticalCenter = Screen.height / 2;

        float left = horizontalCenter - 200;
        float top = verticalCenter - 200;
        float width = 400;
        float height = 250;
        string[] accounts = { "Vartotojas1", "Vartotojas2" };
        GUI.Box(new Rect(menuBoxLeftPosition, top, menuBoxWidth, height), "Paskyrų pasirinkimo langas");
        int topButton = 25;
        for (int i=0; i<accounts.Length; i++)
        {
            if (GUI.Button(new Rect((Screen.width / 10) * 2, top + (topButton + (50 * i)), menuButtonWidth, menuButtonHeight), accounts[i]))
            {
                //
            }
        }
        GUI.Box(new Rect(menuBoxLeftPosition, top+height, menuBoxWidth, 125), "");
        if (GUI.Button(new Rect((Screen.width / 10) * 2, top + height + 5, menuButtonWidth, menuButtonHeight), "Kurti naują paskyrą"))
        {
            //
        }
        if (GUI.Button(new Rect((Screen.width / 10) * 2, top + height + 55, menuButtonWidth, menuButtonHeight), "Grįžti į pagrindinį meniu"))
        {
            //
        }
    }


    public void showInGameMenu()
    {
        float horizontalCenter = Screen.width / 2;
        float verticalCenter = Screen.height / 2;

        float left = horizontalCenter - 200;
        float top = verticalCenter - 200;
        float width = 400;
        float height = 350;

        GUI.Box(new Rect(left, top, width, height), "Meniu");

        if (GUI.Button(new Rect(horizontalCenter - 100, top + 25, 200, 50), "Išjungti menu"))
        {
            //
        }

        if (GUI.Button(new Rect(horizontalCenter - 100, top + 75, 200, 50), "Atstatyti automoblį"))
        {
            //
        }

        if (GUI.Button(new Rect(horizontalCenter - 100, top + 125, 200, 50), "Įkrauti iš naujo"))
        {
            game.restartLevel();
        }

        if (GUI.Button(new Rect(horizontalCenter - 100, top + 175, 200, 50), "Grįžti į pagrindinį meniu"))
        {
            game.returnToMainMenu();
        }


        if (GUI.Button(new Rect(horizontalCenter - 100, top + 225, 200, 50), "Išjungti žaidimą"))
        {
            game.quitGame();
        }
    }

    public void showBestResults()
    {
        float horizontalCenter = Screen.width / 2;
        float verticalCenter = Screen.height / 2;
        float left = horizontalCenter - 200;
        float top = verticalCenter - 200;
        float width = 400;
        float height = 250;
        string[] results = { "Vartotojas1          10000          2014-12-12",
                             "Vartotojas2          90000          2014-12-12",
                             "Vartotojas1          80000          2014-12-12",
                             "Vartotojas1          70000          2014-12-12",
                             "Vartotojas2          60000          2014-12-15"};
        GUI.Box(new Rect(menuBoxLeftPosition, top, menuBoxWidth, height), "Geriausi rezultatai");
        GUI.Label(new Rect(left + 25, top + 25, width, 125), "Nr    Vartotojas        Rezultatas         Data");
        int topButton = 25;
        for (int i = 0; i < results.Length; i++)
        {
            GUI.Label(new Rect(left+25, top+50+(25*i), width, 125), (i+1)+"     "+results[i]);
        }
        GUI.Box(new Rect(menuBoxLeftPosition, top + height, menuBoxWidth, 125), "");
        if (GUI.Button(new Rect(horizontalCenter - 100, top + height + 5, 200, 50), "Valyti rezultatus"))
        {
            //
        }
        if (GUI.Button(new Rect(horizontalCenter - 100, top + height + 55, 200, 50), "Grįžti į pagrindinį meniu"))
        {
            //
        }
    }

}
