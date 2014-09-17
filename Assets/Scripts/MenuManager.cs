using UnityEngine;
using UnityEngine.UI;
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
        GameManager.onPauseActive += PauseActive;
    }

    #region ButtonMethods
    public void PauseBtn()
    {
        GameManager.MainMenu();
    }
    #endregion

    #region Events
    void LevelCompleted()
    {
        completedMenu.SetActive(true);
    }

    void GameActive()
    {
        hud.SetActive(true);
        levelSelect.SetActive(false);
        completedMenu.SetActive(false);
    }

    void MenuActive()
    {
        hud.SetActive(false);
        levelSelect.SetActive(true);
        completedMenu.SetActive(false);
    }

    void PauseActive()
    {
        pauseMenu.SetActive(true);
        hud.SetActive(false);
    }
    #endregion
}
