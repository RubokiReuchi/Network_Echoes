using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum ACTIVE_MENU
{
    MAIN,
    MULTIPLAYER,
    SETTINGS,
    CREDITS
}

public class InitialScreenManager : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject multiplayerMenu;
    public GameObject settingsMenu;
    public GameObject creditsMenu;
    public GameObject mainMenuFirstButton;
    public GameObject multiplayerMenuFirstButton;
    public GameObject settingsMenuFirstButton;

    GameObject lastSelect;

    // Start is called before the first frame update
    void Start()
    {
        mainMenu.SetActive(true);
        multiplayerMenu.SetActive(false);
        settingsMenu.SetActive(false);
        creditsMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (EventSystem.current.currentSelectedGameObject == null) EventSystem.current.SetSelectedGameObject(lastSelect);
        else lastSelect = EventSystem.current.currentSelectedGameObject;
    }

    public void OpenMenu(ACTIVE_MENU menuToOpen)
    {
        CloseAllMenus();
        switch (menuToOpen)
        {
            case ACTIVE_MENU.MAIN:
                mainMenu.SetActive(true);
                EventSystem.current.SetSelectedGameObject(mainMenuFirstButton);
                break;
            case ACTIVE_MENU.MULTIPLAYER:
                multiplayerMenu.SetActive(true);
                EventSystem.current.SetSelectedGameObject(multiplayerMenuFirstButton);
                break;
            case ACTIVE_MENU.SETTINGS:
                settingsMenu.SetActive(true);
                EventSystem.current.SetSelectedGameObject(settingsMenuFirstButton);
                break;
            case ACTIVE_MENU.CREDITS:
                creditsMenu.SetActive(true);
                break;
            default:
                break;
        }
    }

    public void CloseAllMenus()
    {
        mainMenu.SetActive(false);
        multiplayerMenu.SetActive(false);
        settingsMenu.SetActive(false);
        creditsMenu.SetActive(false);
    }
}
