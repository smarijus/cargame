using UnityEngine;
using System.Collections;

public class UserInterface
{
    private Game game = new Game();

    public void showSpeed(float speed)
    {
        GUI.Label(new Rect(0, Screen.height - 50, 200, 100), "Automobio greitis: " + speed.ToString());
    }

    public void showCurrentProfile()
    {
        float horizontalCenter = Screen.width / 2;
        float verticalCenter = Screen.height / 2;

        float left = horizontalCenter - 200;
        float top = verticalCenter - 200;
        float width = 400;
        float height = 100;

        GUI.Box(new Rect(left, top, width, height), "Vartotojo paskyra: ");
        if (GUI.Button(new Rect(horizontalCenter - 100, verticalCenter - 175, 200, 50), game.getUserProfile()))
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
        float width = 400;
        float height = 250;

        GUI.Box(new Rect(left, top, width, height), "Pagrindinis meniu");

        if (GUI.Button(new Rect(horizontalCenter - 100, verticalCenter - 75, 200, 50), "Pradėti žaidimą"))
        {
            game.loadGameScene("GameScene");
        }

        if (GUI.Button(new Rect(horizontalCenter - 100, verticalCenter - 25, 200, 50), "Tęsti išsaugotą žaidimą"))
        {
            //game.quitGame();
        }

        if (GUI.Button(new Rect(horizontalCenter - 100, verticalCenter + 25, 200, 50), "Informacija apie projektą"))
        {
            game.quitGame();
        }

        if (GUI.Button(new Rect(horizontalCenter - 100, verticalCenter + 75, 200, 50), "Išjungti žaidimą"))
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
        GUI.Box(new Rect(left, top, width, height), "Paskyrų pasirinkimo langas");
        int topButton = 25;
        for (int i=0; i<accounts.Length; i++)
        {
            if (GUI.Button(new Rect(horizontalCenter - 100, top + (topButton + (50 * i)), 200, 50), accounts[i]))
            {
                //
            }
        }
        GUI.Box(new Rect(left, top+height, width, 125), "");
        if (GUI.Button(new Rect(horizontalCenter - 100, top + height+5, 200, 50), "Kurti naują paskyrą"))
        {
            //
        }
        if (GUI.Button(new Rect(horizontalCenter - 100, top + height + 55, 200, 50), "Grįžti į pagrindinį meniu"))
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
        GUI.Box(new Rect(left, top, width, height), "Geriausi rezultatai");
        GUI.Label(new Rect(left + 25, top + 25, width, 125), "Nr    Vartotojas        Rezultatas         Data");
        int topButton = 25;
        for (int i = 0; i < results.Length; i++)
        {
            GUI.Label(new Rect(left+25, top+50+(25*i), width, 125), (i+1)+"     "+results[i]);
        }
        GUI.Box(new Rect(left, top + height, width, 125), "");
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
