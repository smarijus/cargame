using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;

public class UserInterface
{
    //private Game game = new Game();

    // Meniu mygtukų plotis, pagal ekrano dydį
    private int menuButtonWidth = (Screen.width / 10) * 6;
    // Meniu mygtukų aukštis, pagal ekrano dydį
    private int menuButtonHeight = (Screen.height / 20) * 2;
    // Meniu mygtukų pozicija nuo kairės
    private int menuButtonLeftPosition = (Screen.width / 10) * 2;
    // Meniu lentelių pozicija nuo kairės
    private int menuBoxLeftPosition = Screen.width / 10;
    // Meniu lentelių plotis, pagal ekrano dydį
    private int menuBoxWidth = (Screen.width / 10) * 8;
    // Koordinatės reikalingos nurodyti paslinkimo kiekį paslenkačiuose languose.
    private Vector2 scrollPosition = Vector2.zero;

    float horizontalCenter = Screen.width / 2;
    float verticalCenter = Screen.height / 2;

    private string userName = "";

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

    // Funkcija atvaizduoja automobilio valdymo mygtukus ekrane
    public void showInGameControls()
    {
        GUI.Button(new Rect(Screen.width / 50, Screen.height / 25, Screen.width / 10, Screen.height / 10), "M");
        GUI.Button(new Rect((Screen.width / 10 * 9) - Screen.width / 50, Screen.height / 25, Screen.width / 10, Screen.height / 10), "C");
        GUI.Button(new Rect(Screen.width / 50, verticalCenter, Screen.width / 10, Screen.height / 5), "H");
        GUI.Button(new Rect(Screen.width / 50, Screen.height - Screen.height / 25 - Screen.height / 5, Screen.width / 10, Screen.height / 5), "B");
        GUI.Button(new Rect((Screen.width / 10 * 9) - Screen.width / 50, (Screen.height / 3 * 2) - Screen.width / 50, Screen.width / 10, Screen.height / 3), "A");
        //GUI.DrawTexture(new Rect(10, 10, 60, 60), aTexture, ScaleMode.ScaleToFit, true, 10.0F);
    }

    // Funkcija atvaizduoja mygtuką, su šiuo metu parinko vartotojo vardu.
    public void showCurrentProfile()
    {
        //float horizontalCenter = Screen.width / 2;
        //float verticalCenter = Screen.height / 2;

        float left = horizontalCenter - 200;
        float top = verticalCenter - 200;
        //float width = 400;
        //float height = 100;

        //GUI.Box(new Rect(left, top, width, height), "Vartotojo paskyra: ");
        GUI.Box(new Rect(menuBoxLeftPosition, Screen.height / 20, menuBoxWidth, (Screen.height / 10) * 2), "Vartotojo paskyra: ");
        if (GUI.Button(new Rect(menuButtonLeftPosition, (Screen.height / 20) * 2, menuButtonWidth, menuButtonHeight), Game.Instance.getCurrentUser()))
        {
            Game.Instance.setMenuItem(1);
            //showProfileSelection();
        }

    }

    // Funkcija atvaizduoja pagrindinį meniu.
    public void showMainMenu()
    {
        //float horizontalCenter = Screen.width / 2;
        //float verticalCenter = Screen.height / 2;

        float left = horizontalCenter - 200;
        float top = verticalCenter - 100;
        //float width = 400;
        //float height = 250;

        GUI.Box(new Rect(menuBoxLeftPosition, (Screen.height / 20) * 6, menuBoxWidth, (Screen.height / 20) * 13), "Pagrindinis meniu");

        if (GUI.Button(new Rect(menuButtonLeftPosition, (Screen.height / 20) * 7, menuButtonWidth, menuButtonHeight), "Pradėti žaidimą"))
        {
            Game.Instance.setMenuItem(0);
            Game.Instance.loadGameScene("GameScene");
        }

        if (GUI.Button(new Rect(menuButtonLeftPosition, (Screen.height / 20) * 9, menuButtonWidth, menuButtonHeight), "Tęsti išsaugotą žaidimą"))
        {
            Game.Instance.setMenuItem(0);
        }

        if (GUI.Button(new Rect(menuButtonLeftPosition, (Screen.height / 20) * 11, menuButtonWidth, menuButtonHeight), "Geriausi rezultatai"))
        {
            Game.Instance.setMenuItem(2);
        }

        if (GUI.Button(new Rect(menuButtonLeftPosition, (Screen.height / 20) * 13, menuButtonWidth, menuButtonHeight), "Informacija apie projektą"))
        {
            Game.Instance.setMenuItem(4);
            //game.quitGame();
            //Game.Instance.quitGame();
        }

        if (GUI.Button(new Rect(menuButtonLeftPosition, (Screen.height / 20) * 15, menuButtonWidth, menuButtonHeight), "Išjungti žaidimą"))
        {
            //game.quitGame();
            Game.Instance.quitGame();
        }
    }

    // Funkcija atvaizduoja vartotojo profilių pasirinkimo meniu.
    public void showProfileSelection()
    {
        //float horizontalCenter = Screen.width / 2;
        //float verticalCenter = Screen.height / 2;

        //float left = horizontalCenter - 200;
        //float top = verticalCenter - 200;
        //float width = 400;
        //float height = 250;
        string[] accounts = Game.Instance.getUsersList();
        GUI.Box(new Rect(menuBoxLeftPosition, Screen.height / 20, menuBoxWidth, (Screen.height / 20) * 13), "Paskyrų pasirinkimo langas");
        int topButton = 25;


        scrollPosition = GUI.BeginScrollView(new Rect(menuBoxLeftPosition, (Screen.height / 20) * 3, menuBoxWidth, (Screen.height / 20) * 10), scrollPosition, new Rect(0, 0, menuBoxWidth - 20, (menuButtonHeight+Screen.height/20) * (accounts.Length + 1)));
        for (int i=0; i<accounts.Length; i++)
        {
            if (GUI.Button(new Rect(menuButtonLeftPosition-menuBoxLeftPosition, 0 + ( ((menuButtonHeight + (Screen.height/20)) * i)), menuButtonWidth, menuButtonHeight), accounts[i]))
            {
                Game.Instance.loadUserProfile(accounts[i]);
                Game.Instance.setMenuItem(0);
            }
        }
        GUI.EndScrollView();

        //for (int i=0; i<accounts.Length; i++)
        //{
        //    if (GUI.Button(new Rect(menuButtonLeftPosition, top + (topButton + (50 * i)), menuButtonWidth, menuButtonHeight), accounts[i]))
        //    {
        //        Game.Instance.loadUserProfile(accounts[i]);
        //        Game.Instance.setMenuItem(0);
        //    }
        //}


        GUI.Box(new Rect(menuBoxLeftPosition, (Screen.height / 20) * 14, menuBoxWidth, (Screen.height / 20) * 6), "");
        if (GUI.Button(new Rect(menuButtonLeftPosition, (Screen.height / 20) * 15, menuButtonWidth, menuButtonHeight), "Kurti naują paskyrą"))
        {
            Game.Instance.setMenuItem(3);
        }
        if (Game.Instance.getCurrentUser() != null)
        {
            if (GUI.Button(new Rect(menuButtonLeftPosition, ((Screen.height / 20) * 17), menuButtonWidth, menuButtonHeight), "Grįžti į pagrindinį meniu"))
            {
                Game.Instance.setMenuItem(0);
            }
        }
        else
        {
            if (GUI.Button(new Rect(menuButtonLeftPosition, ((Screen.height / 20) * 17), menuButtonWidth, menuButtonHeight), "Išjungti žaidimą"))
            {
                Game.Instance.quitGame();
            }
        }
    }

    // Funkcija atvaizduoja meniu žaidžiant.
    public void showInGameMenu()
    {
        //float horizontalCenter = Screen.width / 2;
        //float verticalCenter = Screen.height / 2;

        float left = horizontalCenter - 200;
        float top = verticalCenter - 200;
        //float width = 400;
        float height = 350;

        GUI.Box(new Rect(menuBoxLeftPosition, (Screen.height / 20) * 2, menuBoxWidth, (Screen.height / 20) * 14), "Meniu");

        if (GUI.Button(new Rect(menuButtonLeftPosition, (Screen.height / 20) * 3, menuButtonWidth, menuButtonHeight), "Išjungti menu"))
        {
            Game.Instance.setInGameMenuStatus(false);
        }

        if (GUI.Button(new Rect(menuButtonLeftPosition, (Screen.height / 20) * 5, menuButtonWidth, menuButtonHeight), "Atstatyti automoblį"))
        {
            Game.Instance.setCarResetStatus(true);
        }

        if (GUI.Button(new Rect(menuButtonLeftPosition, (Screen.height / 20) * 7, menuButtonWidth, menuButtonHeight), "Įkrauti iš naujo"))
        {
            //game.restartLevel();
            Game.Instance.restartLevel();
        }

        if (GUI.Button(new Rect(menuButtonLeftPosition, (Screen.height / 20) * 9, menuButtonWidth, menuButtonHeight), "Išsaugoti ir grįžti į pagrindinį meniu"))
        {
            // game.returnToMainMenu();
            //Game.Instance.setMenuItem(1);
            Game.Instance.returnToMainMenu();
        }

        if (GUI.Button(new Rect(menuButtonLeftPosition, (Screen.height / 20) * 11, menuButtonWidth, menuButtonHeight), "Grįžti į pagrindinį meniu"))
        {
           // game.returnToMainMenu();
            //Game.Instance.setMenuItem(1);
            Game.Instance.returnToMainMenu();
        }


        if (GUI.Button(new Rect(menuButtonLeftPosition, (Screen.height / 20) * 13, menuButtonWidth, menuButtonHeight), "Išjungti žaidimą"))
        {
            //game.quitGame();
            Game.Instance.quitGame();
        }
    }

    // Funkcija atvaizduoja geriausius rezultatus.
    public void showBestResults()
    {
        //float horizontalCenter = Screen.width / 2;
        //float verticalCenter = Screen.height / 2;
        float left = horizontalCenter - 200;
        float top = verticalCenter - 200;
        float width = 400;
        float height = 250;
        string[] highscores = Game.Instance.getHighscoresList();
        GUI.Box(new Rect(menuBoxLeftPosition   , Screen.height/20, menuBoxWidth, (Screen.height/20)*12), "Geriausi rezultatai");
        GUI.Label(new Rect(menuBoxLeftPosition + (Screen.width/20)      , (Screen.height / 20) * 2 , (Screen.width/20)*2, Screen.height / 20), "Nr");
        GUI.Label(new Rect(menuBoxLeftPosition + (Screen.width / 20) * 3, (Screen.height / 20) * 2, (Screen.width / 20) * 4, Screen.height / 20), "Vartotojas");
        GUI.Label(new Rect(menuBoxLeftPosition + (Screen.width / 20) * 8, (Screen.height / 20) * 2, (Screen.width / 20) * 4, Screen.height / 20), "Rezultatas");
        GUI.Label(new Rect(menuBoxLeftPosition + (Screen.width / 20) * 14, (Screen.height / 20) * 2, (Screen.width / 20) * 4, Screen.height / 20), "Data");
        int topButton = 25;
        scrollPosition = GUI.BeginScrollView(new Rect(menuBoxLeftPosition, (Screen.height / 20) * 3, menuBoxWidth, (Screen.height / 20) * 12), scrollPosition, new Rect(0, 0, menuBoxWidth, menuButtonHeight * (highscores.Length + 1)));
        for (int i = 0; i < highscores.Length; i++)
        {
            string[] words = Regex.Split(highscores[i], " ");
            //GUI.Label(new Rect(left+25, top+50+(25*i), width, 125), (i+1)+"     "+highscores[i]);
            GUI.Label(new Rect((Screen.width / 20)     , ((Screen.height / 20) * i), (Screen.width / 20) * 2, Screen.height / 20), (i+1).ToString());
            GUI.Label(new Rect((Screen.width / 20) * 3 , ((Screen.height / 20) * i), (Screen.width / 20) * 4, Screen.height / 20), words[0]);
            GUI.Label(new Rect((Screen.width / 20) * 8, ((Screen.height / 20) * i), (Screen.width / 20) * 4, Screen.height / 20), words[1]);
            GUI.Label(new Rect((Screen.width / 20) * 14, ((Screen.height / 20) * i), (Screen.width / 20) * 4, Screen.height / 20), words[2]);
        }
        GUI.EndScrollView();
        GUI.Box(new Rect(menuBoxLeftPosition, (Screen.height/20)*13, menuBoxWidth, (Screen.height/20)*6), "");
        if (GUI.Button(new Rect(menuButtonLeftPosition, (Screen.height / 20) * 14, menuButtonWidth, menuButtonHeight), "Valyti rezultatus"))
        {
            Game.Instance.clearHigscores();
        }
        if (GUI.Button(new Rect(menuButtonLeftPosition, (Screen.height / 20) * 16, menuButtonWidth, menuButtonHeight), "Grįžti į pagrindinį meniu"))
        {
            Game.Instance.setMenuItem(0);
        }
    }

    // Funkcija atvaizduoja meniu skirtą kurti naujus vartotoj profilius.
    public void showProfileCreationMenu()
    {
        //string userName;

        float horizontalCenter = Screen.width / 2;
        float verticalCenter = Screen.height / 2;

        float left = horizontalCenter - 200;
        float top = verticalCenter - 200;
        float height = 250;
        string[] userList = Game.Instance.getUsersList();
        GUI.Box(new Rect(menuBoxLeftPosition, (Screen.height/20), menuBoxWidth, (Screen.height/20)*13), "Kurti naują paskyrą");
        userName = GUI.TextField(new Rect(menuButtonLeftPosition, (Screen.height / 20)*2, menuButtonWidth, menuButtonHeight), userName, 25);
        int topButton = 25;
        GUI.Box(new Rect(menuBoxLeftPosition, (Screen.height / 20) * 14, menuBoxWidth, (Screen.height / 20) * 6), "");
        if (GUI.Button(new Rect(menuButtonLeftPosition, (Screen.height / 20) * 15, menuButtonWidth, menuButtonHeight), "Išsaugoti"))
        {
            Game.Instance.createNewUserProfile(userName);
            userName = "";
            Game.Instance.setMenuItem(1);
            //game.createNewProfile();
        }
        if (GUI.Button(new Rect(menuButtonLeftPosition, (Screen.height / 20) * 17, menuButtonWidth, menuButtonHeight), "Grįžti į paskyrų sąrašą"))
        {
            Game.Instance.setMenuItem(1);
        }
    }

    // Funkcija atvaizduoja informaciją apie projektą.
    public void showProjectInfo()
    {
        float horizontalCenter = Screen.width / 2;
        float verticalCenter = Screen.height / 2;
        float left = horizontalCenter - 200;
        float top = verticalCenter - 200;
        float width = 400;
        float height = 250;
        GUI.Box(new Rect(menuBoxLeftPosition, Screen.height / 20, menuBoxWidth, (Screen.height / 20)*15), "Informacija apie projektą");


        //scrollPosition = GUI.BeginScrollView(new Rect(menuBoxLeftPosition, top + 50, 100, 100), scrollPosition, new Rect(0, 0, 100, 400));
        //GUI.Button(new Rect(0, 0, 100, 20), "Top-left");
        //GUI.Button(new Rect(120, 0, 100, 20), "Top-right");
        //GUI.Button(new Rect(0, 180, 100, 20), "Bottom-left");
        //GUI.Button(new Rect(120, 240, 100, 20), "Bottom-right");
        //GUI.Button(new Rect(120, 300, 100, 20), "Bottom-right");
        //GUI.Button(new Rect(120, 360, 100, 20), "Bottom-right");
        //GUI.Button(new Rect(120, 400, 100, 20), "Bottom-right");
        //GUI.EndScrollView();

       // GUI.Label(new Rect(left + 25, top + 25, width, 125), "Nr    Vartotojas        Rezultatas         Data");
        //int topButton = 25;
        //for (int i = 0; i < results.Length; i++)
       /// {
        //    GUI.Label(new Rect(left + 25, top + 50 + (25 * i), width, 125), (i + 1) + "     " + results[i]);
       // }
        GUI.Box(new Rect(menuBoxLeftPosition, (Screen.height/20) * 16, menuBoxWidth, (Screen.height/20)*4), "");
        //if (GUI.Button(new Rect(menuButtonLeftPosition, top + height + 5, menuButtonWidth, menuButtonHeight), "Valyti rezultatus"))
        //{
        //    //
        //}
        if (GUI.Button(new Rect(menuButtonLeftPosition, (Screen.height/20)*17, menuButtonWidth, menuButtonHeight), "Grįžti į pagrindinį meniu"))
        {
            Game.Instance.setMenuItem(0);
        }
    }

    //static string[] GetWords(string input)
    //{
    //    MatchCollection matches = Regex.Matches(input, @" ");

    //    var words = from m in matches.Cast<Match>()
    //                where !string.IsNullOrEmpty(m.Value)
    //                select TrimSuffix(m.Value);

    //    return words.ToArray();
    //}

    //static string TrimSuffix(string word)
    //{
    //    int apostropheLocation = word.IndexOf(' ');
    //    if (apostropheLocation != -1)
    //    {
    //        word = word.Substring(0, apostropheLocation);
    //    }

    //    return word;
    //}

}
