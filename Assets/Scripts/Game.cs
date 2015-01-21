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
        //if (currentUser == null)

    }


    // Kintamasis skirtas saugoti dabartinima vartotojo vardui
    private string currentUser;
    // Kintamasis skirtas nurodyti reikiamą meniu punktą
    private int currentMenuItem = 0;
    // Kintamasis, kuris nurodo ar žaidimo meniu įjungtas
    private bool inGameMenuEnabled = false;
    // Kintamasis, kuris nurodo, kad reikia pastatyti automobilį į pradinę padėtį.
    private bool resetCarStatus = false;
    FileSystem fileSystem = new FileSystem();


    public void loadDB()
    {
        loadUserList();
    }
    string[] userList;// = { "Vartotojas1", "Vartotojas2" };
    
    //public void getList()
    //{
    //    userList = fileSystem.getUsersList();
    //    //Debug.Log(userList);
    //}




    // Funkcija užkrauna nurodytą žaidimo sceną.
    // Parametrai:
    //              sceneName - sceno pavadinimas;
    public void loadGameScene(string sceneName)
    {
        Application.LoadLevel(sceneName);
    }

    // Funkcija skirta pradėto žaisti naują žaidimą.
    public void startNewGame()
    {
        Application.LoadLevel("GameScene");
    }

    // Funkcija skirta atvaizduoti meniu sceną.
    public void returnToMainMenu()
    {
        Application.LoadLevel("MainMenuScene");
    }

    // Funkcija skirta perkrauti žaidimą iš naujo.
    public void restartLevel()
    {
        Application.LoadLevel(Application.loadedLevelName);
    }

    // Funkcija skirta išjunti žaidimą.
    public void quitGame()
    {
        Application.Quit();
    }

    //-----------------------------------------------------------------
    //Funkcija neuzžbaigta
    
    public void createNewUserProfile(string userName)
    {
        //string[] tempList = new string[userList.Length + 1];
        //for (int i = 0; i < userList.Length; i++)
        //    tempList[i] = userList[i];
        //tempList[tempList.Length - 1] = name;
        //userList = tempList;
        fileSystem.addNewUser(userName);
        loadUserList();
    }

    public void loadUserList()
    {
        userList = fileSystem.getUsersList();
    }

    //----------------------------------------------------------------
    // Funcija neužbaigta
    // Funkcija skirta įkrauti pasirinkto vartotojo profilio duomenis.
    public void loadUserProfile(string name)
    {
        currentUser = name;
    }

    // Funkcija kuri grąžina vartotojų profilių sąrašą.
    public string[] getUsersList()
    {
        
        return userList;
    }

    // Funkcija grąžina dabartinio vartotojo vardą.
    public string getCurrentUser()
    {
        return currentUser;
    }

    // Funkcija nurodo, kurį meniu punktą reikia atvaizduoti.
    public int getMenuItem()
    {
        if (currentUser == null && currentMenuItem != 3)
            return 1;
        else
            if (currentUser == null && currentMenuItem == 3)
                return 3;
            else
                return currentMenuItem;
    }

    // Funkcija skirta nurodyti, kuris meniu punktas bus atvaizduojamas
    // Parametrai:
    //              menuItem - numeris, nurodantis meniu punktą;  
    public void setMenuItem(int menuItem)
    {
        currentMenuItem = menuItem;
    }

    // Funkcija, kuri grąžina ar įjungtas žaidimo meniu.
    public bool getInGameMenuStatus()
    {
        return inGameMenuEnabled;
    }

    // Funkcija kuri nurodo žaidimo meniu įjungimo būseną.
    // Parametrai:
    //              status - žaidimo meniu būsena;
    public void setInGameMenuStatus(bool status)
    {
        inGameMenuEnabled = status;
    }

    // Funkcija kuri nurodo ar reikia nustatyti automobilį į pradinę padėtį.
    public bool getCarResetStatus()
    {
        return resetCarStatus;
    }

    // Funkcija kuri nurodo naują reikšmę, parametrui, skirtam nurodyti ar reikia pastatyt atumobilį į pradinę padadėtį.
    // Parametrai:
    //              status - nauja būsena;
    public void setCarResetStatus(bool status)
    {
        resetCarStatus = status;
    }
}
