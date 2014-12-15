using UnityEngine;
using System.Collections;

public class UserInterface
{
    private Game game = new Game();

    public void showSpeed(float speed)
    {
        GUI.Label(new Rect(0, Screen.height - 50, 200, 100), "Automobio greitis: " + speed.ToString());
    }

    public void showMenu()
    {
        float horizontalCenter = Screen.width / 2;
        float verticalCenter = Screen.height / 2;

        float left = horizontalCenter - 200;
        float top = verticalCenter - 100;
        float width = 400;
        float height = 250;

        GUI.Box(new Rect(left, top, width, height), "Meniu");

        if (GUI.Button(new Rect(horizontalCenter - 100, verticalCenter - 75, 200, 50), "Išjungti menu"))
        {
            //
        }


        if (GUI.Button(new Rect(horizontalCenter - 100, verticalCenter - 25, 200, 50), "Įkrauti iš naujo"))
        {
            game.loadGameScene(Application.loadedLevelName);
        }

        if (GUI.Button(new Rect(horizontalCenter - 100, verticalCenter + 25, 200, 50), "Grįžti į pagrindinį meniu"))
        {
            game.loadGameScene("MainMenuScene");
        }


        if (GUI.Button(new Rect(horizontalCenter - 100, verticalCenter + 75, 200, 50), "išjungti"))
        {
            Application.Quit();
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

        if (GUI.Button(new Rect(horizontalCenter - 100, verticalCenter - 25, 200, 50), "Pradėti žaidimą"))
        {
            game.loadGameScene("GameScene");
        }

        if (GUI.Button(new Rect(horizontalCenter - 100, verticalCenter + 75, 200, 50), "Išjungti žaidimą"))
        {
            Application.Quit();
        }
    }
}
