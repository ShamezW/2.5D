using UnityEngine;
using System.Collections;

public class GameManager : Singleton<GameManager> {
    public Animator cameraAnimator;
    private bool orthoToggle = false;

    void Update()
    {
        if (GestureManager.State == GestureState.DoubleTap)
            ToggleOrthoMode();
    }

    public void ToggleOrthoMode()
    {
        if (!Instance.orthoToggle)
        {
            Instance.cameraAnimator.SetTrigger("Ortho");
            Instance.orthoToggle = true;
        }
        else
        {
            Instance.cameraAnimator.SetTrigger("Persp");
            Instance.orthoToggle = false;
        }
    }

    public void PauseGame()
    {
        Debug.Log("Paused");
    }
}
