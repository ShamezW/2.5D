using UnityEngine;
using System.Collections;

public class MenuManager : Singleton<MenuManager> {
    public void PauseMenu()
    {
        GameManager.Instance.PauseGame();
    }
}
