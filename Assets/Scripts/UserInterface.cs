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

        GUI.Box(new Rect(left, top, width, height), "Vartotojo profilis: ");
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

        if (GUI.Button(new Rect(horizontalCenter - 100, verticalCenter - 25, 200, 50), "Pradėti žaidimą"))
        {
            game.loadGameScene("GameScene");
        }

        if (GUI.Button(new Rect(horizontalCenter - 100, verticalCenter + 75, 200, 50), "Išjungti žaidimą"))
        {
            game.quitGame();
        }
    }

    private void showProfileSelection()
    {
        float horizontalCenter = Screen.width / 2;
        float verticalCenter = Screen.height / 2;

        float left = horizontalCenter - 200;
        float top = verticalCenter - 100;
        float width = 400;
        float height = 250;

        GUI.Box(new Rect(left, top, width, height), "Profilių pasirinkimo langas");

    }


    public void showInGameMenu()
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
            game.restartLevel();
        }

        if (GUI.Button(new Rect(horizontalCenter - 100, verticalCenter + 25, 200, 50), "Grįžti į pagrindinį meniu"))
        {
            game.returnToMainMenu();
        }


        if (GUI.Button(new Rect(horizontalCenter - 100, verticalCenter + 75, 200, 50), "Išjungti žaidimą"))
        {
            game.quitGame();
        }
    }


}
