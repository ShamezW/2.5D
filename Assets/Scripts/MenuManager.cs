using UnityEngine;
using System.Collections;

public class MenuManager : Singleton<MenuManager> {
    public GameObject completedMenu;

    public void PauseMenu()
    {
    }

    void Start()
    {
        GameManager.onLevelCompleated += LevelCompleted;
    }

    void LevelCompleted()
    {
        completedMenu.SetActive(true);
    }
}
