using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour
{

    public GUISkin menuSkin;
    private bool mainMenuShown = true;
    private bool profileSelectionShown = false;
    private bool bestResultsShown = false;
    private bool profileCreationMenuShown = false;
    private int menuItem = 4;
    private UserInterface ui = new UserInterface();

    void OnGUI()
    {
        GUI.skin = menuSkin;
        switch (menuItem)
        {
            case 1:
                ui.showCurrentProfile();
                ui.showMainMenu();
                break;
            case 2:
                ui.showProfileSelection();
                break;
            case 3:
                ui.showBestResults();
                break;
            case 4:
                ui.showProfileCreationMenu();
                break;
        }
        //if (mainMenuShown)
        //{
        //    ui.showCurrentProfile();
        //    ui.showMainMenu();
        //}
        //if (profileSelectionShown)
        //    ui.showProfileSelection();
        //if (bestResultsShown)
        //    ui.showBestResults();
        //if (profileCreationMenuShown)
        //    ui.showProfileCreationMenu();
    }
}
