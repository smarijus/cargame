using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour
{

    public GUISkin menuSkin;
    private bool mainMenuShown = true;
    private bool profileSelectionShown = false;
    private bool bestResultsShown = false;
    private bool profileCreationMenuShown = false;
    private int menuItem = 1;
    private UserInterface ui = new UserInterface();
    //private Game test = new Game();

    void Start()
    {
        //DontDestroyOnLoad(Game.Instance);
        DontDestroyOnLoad(Game.Instance);
        Game.Instance.startState();
    }

    void OnGUI()
    {
        GUI.skin = menuSkin;
        switch (Game.Instance.getMenuItem())
        {
            case 0:
                ui.showCurrentProfile();
                ui.showMainMenu();
                break;
            case 1:
                ui.showProfileSelection();
                break;
            case 2:
                ui.showBestResults();
                break;
            case 3:
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
