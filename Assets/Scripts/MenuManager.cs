using UnityEngine;
using System.Collections;

public class MenuManager : Singleton<MenuManager> {
    public GameObject
        completedMenu,
        pauseMenu,
        levelSelect,
        hud,
        mainMenu;

    void Start()
    {
        //sub to events
        GameManager.onLevelCompleated += LevelCompleted;
        GameManager.onGameActive += GameActive;
        GameManager.onMenuActive += MenuActive;
    }


    //Triggered on events
    void LevelCompleted()
    {
        completedMenu.SetActive(true);
    }

    void GameActive()
    {
        hud.SetActive(true);
        levelSelect.SetActive(false);
    }

    void MenuActive()
    {
        hud.SetActive(false);
        levelSelect.SetActive(true);
    }
}
