using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour
{

    public GUISkin menuSkin;

    void OnGUI()
    {
        GUI.skin = menuSkin;
        UserInterface ui = new UserInterface();
        ui.showMainMenu();
    }
}
