using UnityEngine;
using System.Collections;

public class LoginMenu : MonoBehaviour {
    string userName = "";
    string password = "";
    string problem = "";

    void OnGUI()
    {
        GUI.Window(0, new Rect(Screen.width / 3, Screen.height / 4, Screen.width / 3, Screen.height / 2 - 70),
            LoginWindow, "Prisijungimas");
    }

    void LoginWindow(int windowID)
    {
        GUI.Label(new Rect(140, 40, 130, 100), "Vartotojo vardas");
        userName = GUI.TextField(new Rect(25, 60, 375, 30), userName);
        GUI.Label(new Rect(140, 92, 130, 100), "Slaptažodis");
        password = GUI.TextField(new Rect(25, 115, 375, 30), password);

        if (GUI.Button(new Rect(25, 160, 175, 50), "Prisijungti"))
            handleLogin(userName, password);
        if(GUI.Button (new Rect(225, 160, 175, 50), "Registruotis"))
            handleRegister(userName, password);
        GUI.Label (new Rect(55, 222, 250, 100), problem);
    }

    void handleLogin(string mUserName, string mPassword)
    {
        var ds = new DataService("database.db");
        ds.CreateDB();
        Person person = ds.getUser(mUserName);
        if (person != null)
        {
            problem = person.Name;
        }
        else
        {
            problem = "Patikrinkite vartotojo varda ir slaptažodį";
        }

    }

    void handleRegister(string mUserName, string mPassword)
    {

    }

}
