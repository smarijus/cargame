using UnityEngine;
using System.Collections;


//public enum GameState
//{
//    MainMenu,
//    Game,

//}

public class Game : MonoBehaviour
{

    //protected Game()
    //{
    //}

    private static Game _instance = null;

    // Singleton pattern implementation
    public static Game Instance
    {
        get
        {
            if (Game._instance == null)
            {
                Game._instance = new GameObject("Game").AddComponent<Game>();
            }
            return Game._instance;
        }
    }

    public void OnApplicationQuit()
    {
        _instance = null;
    }

    public void startState()
    {
        
    }



    private string currentUser;
    private int currentMenuItem = 0;

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


    public int getMenuItem()
    {
        return currentMenuItem;
    }

    public void setMenuItem(int menuItem)
    {
        currentMenuItem = menuItem;
    }
    
}
