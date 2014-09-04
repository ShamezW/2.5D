using UnityEngine;
using System.Collections;

public class GameManager : Singleton<GameManager> {
    public Animator cameraAnimator;
    public static bool orthoToggle = false;

    void Start()
    {
        LevelData[] levels = Resources.LoadAll<LevelData>("Levels");
        levels[0].CreateLevel();
    }

    void Update()
    {
        if (GestureManager.State == GestureState.DoubleTap)
            ToggleOrthoMode();
    }

    public void ToggleOrthoMode()
    {
        if (!orthoToggle)
        {
            Instance.cameraAnimator.SetTrigger("Ortho");
            orthoToggle = true;
        }
        else
        {
            Instance.cameraAnimator.SetTrigger("Persp");
            orthoToggle = false;
        }
    }

    public void PauseGame()
    {
        Debug.Log("Paused");
    }
}
