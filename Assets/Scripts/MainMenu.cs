using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour
{

    public GUISkin menuSkin;
    private bool mainMenuShown = true;
    private bool profileSelectionShown = false;
    private bool bestResultsShown = false;

    void OnGUI()
    {
        GUI.skin = menuSkin;
        UserInterface ui = new UserInterface();
        if (mainMenuShown)
        {
            ui.showCurrentProfile();
            ui.showMainMenu();
        }
        if (profileSelectionShown)
            ui.showProfileSelection();
        if (bestResultsShown)
            ui.showBestResults();
    }
}
