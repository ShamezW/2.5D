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
        GameManager.PauseGame();
    }

    public void Return()
    {
        GameManager.ResumeGame();
    }

    public void Restart()
    {
        GameManager.ReloadLevel();
    }

    public void MainMenu()
    {
        GameManager.MainMenu();
    }

    public void NextLevel()
    {
        GameManager.NextLevel();
    }
    #endregion

    #region Events
    void LevelCompleted()
    {
        completedMenu.SetActive(true);
        hud.SetActive(false);
    }

    void GameActive()
    {
        hud.SetActive(true);
        levelSelect.SetActive(false);
        completedMenu.SetActive(false);
        pauseMenu.SetActive(false);
    }

    void MenuActive()
    {
        hud.SetActive(false);
        levelSelect.SetActive(true);
        completedMenu.SetActive(false);
        pauseMenu.SetActive(false);
    }

    void PauseActive()
    {
        pauseMenu.SetActive(true);
        hud.SetActive(false);
    }
    #endregion
}
